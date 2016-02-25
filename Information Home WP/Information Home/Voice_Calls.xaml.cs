using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using System.IO;
using System.Diagnostics;

namespace Information_Home
{
    public partial class Voice_Calls : PhoneApplicationPage
    {
        WP_Client Client = null;
        Microphone microphone = Microphone.Default;
        private SoundEffectInstance soundInstance;
        Thread threadInvite;
        Thread threadVoice;
        Thread threadReceive;
        byte[] buffer;
        byte[] Rec;
        int key = 0;
        string type = "";
        int Voice = 0;
        public Voice_Calls()
        {
            InitializeComponent();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(33);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();

            // 获取音频数据缓冲区满时，事件处理程序
            microphone.BufferReady += new EventHandler<EventArgs>(microphone_BufferReady);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Client = Connect.Client;
            threadReceive = new Thread(new ThreadStart(Show_Sound));
            Invite_Click();
            changlanguage();
        }
        void changlanguage()
        {
            if (Information.Language == 0)
            {
                this.btShow.Text = "等待回复";
                this.tu.Content = "退出语音";
                this.tu.FontSize = 35;
            }
            else if (Information.Language == 1)
            {
                this.btShow.Text = "Waiting for a reply";
                this.tu.Content = "To exit voice";
                this.tu.FontSize = 25;
            }
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (key == 1)
            {
                microphone.Stop();
                threadVoice.Abort();
                string stop = "|||***|||";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(stop.ToString());
                Client.SendCommand(buffer);
            }
            base.OnNavigatedFrom(e);
        }
        void dt_Tick(object sender, EventArgs e)
        {
            try { FrameworkDispatcher.Update(); }
            catch { }
        }
        void microphone_BufferReady(object sender, EventArgs e)
        {
            MemoryStream s = new MemoryStream();
            // 检索音频数据
            microphone.GetData(buffer);
            // 存储音频数据流中的
            s.Write(buffer, 0, buffer.Length);
            Client.SendCommand(s.ToArray());

        }
        public void StartVoice()//调用发送函数与接收函数
        {
            microphone.BufferDuration = TimeSpan.FromMilliseconds(500);
            // 分配内存以保存音频数据
            buffer = new byte[microphone.GetSampleSizeInBytes(microphone.BufferDuration)];
            // 开始记录
            microphone.Start();
        }
        private void Close_Voice(object sender, RoutedEventArgs e)
        {
            if(Voice == 1)
            {
                NavigationService.GoBack();
            }
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (Voice == 1)
            {
                NavigationService.GoBack();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Invite_Click()
        {
            threadInvite = new Thread(Invite);                            //实例化线程
            threadInvite.IsBackground = true;
            threadInvite.Start();
        }
        private void Invite()
        {
            Client.SendCommand("voice?");
            string rec = Client.Receive(0);
            Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                if (rec == "OK")
                {
                    key = 1;
                    MessageBox.Show("对方接受语音通话！");
                    threadInvite.Abort();
                    threadReceive.Start();
                }
                else
                {
                    MessageBox.Show("对方拒绝！");
                    threadInvite.Abort();
                }
            });
        }


        private void Send_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(Voice == 1){
                string stop = "start";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(stop.ToString());
                Client.SendCommand(buffer);
                microphone.Stop();
                threadVoice.Abort();
                Voice = 0;
                threadReceive = new Thread(new ThreadStart(Show_Sound));
                threadReceive.Start();
            }
        }
        public void Show_Sound()
        {
            while (true)
            {
                Rec = Client.Receive(1024000, 1024000);
                type = System.Text.Encoding.UTF8.GetString(Rec, 0, 5);
                if (type != "start")
                {
                    Dispatcher.BeginInvoke((ThreadStart)delegate()
                    {
                        this.btShow.Text = "T语言中";
                        this.tishi.Text = "";
                        SoundEffect sound = new SoundEffect(Rec, 8100, AudioChannels.Mono);
                        soundInstance = sound.CreateInstance();
                        soundInstance.Volume = 0.7F;
                        soundInstance.Play();
                    });
                }
                else if (type == "start")
                {
                    threadVoice = new Thread(new ThreadStart(StartVoice));
                    threadVoice.IsBackground = true;
                    threadVoice.Start();                                  //发送声音线程开启
                    Voice = 1;                            //判断发送线程是否开启
                    Dispatcher.BeginInvoke((ThreadStart)delegate()
                    {
                        this.btShow.Text = "W语言中";
                        this.tishi.Text = "点击转换";
                    });
                    threadReceive.Abort();
                }
            }

        }

    }
}