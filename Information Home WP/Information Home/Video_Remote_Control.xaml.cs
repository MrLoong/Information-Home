using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Information_Home.Resources;
using System.Diagnostics;
class Now_Key                            //头名字
{
    public string now_key { get; set; }
    public Now_Key(string now)
    {
        now_key = now;
    }
    public override string ToString()
    {
        return now_key;
    }
}

namespace Information_Home
{
    public partial class Video_Remote_Control : PhoneApplicationPage
    {
        GameTimer timer;
        WPAccHelper accHelper;
        WP_Client Client = null;
        int key = 0;
        int value = 0;
        int Control = 0;
        public Video_Remote_Control()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Client = Connect.Client;
            changlanguage();
            this.sign.Content = new Now_Key("打开手势");
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            accHelper = new WPAccHelper();
            accHelper.SupportedAction = ActionEnum.LeftShake | ActionEnum.UpShake | ActionEnum.RightShake | ActionEnum.DownShake;
        }
        void changlanguage()
        {
            if (Information.Language == 0)
            {
                this.shipingyaokong.Text = "视频遥控";
                this.shipingyaokong.FontSize=60;
                this.tuichu.Content = "退出";
                this.tuichu.FontSize = 45;
            }
            else if (Information.Language == 1)
            {
                this.shipingyaokong.Text = "Video remote control";
                this.shipingyaokong.FontSize = 30;
                this.tuichu.Content = "Close";
                this.tuichu.FontSize = 40;
            }
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            key = 0;
            value = 0;
            Control = 0;
            timer.Update -= OnUpdate;
            base.OnNavigatedFrom(e);
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)             //物理返回键
        {
            NavigationService.GoBack();
        }
        private void OnBackKey(object sender, RoutedEventArgs e)                   //退出
        {
            NavigationService.GoBack();
        }
        private void On_Off_Click(object sender, System.Windows.Input.MouseEventArgs e)              //打开播放器
        {
            if (key == 0)
            {
                key = 1;
                Client.SendCommand(@"10|O");
            }
            else if (key == 1)
            {
                key = 0;
                Client.SendCommand(@"10|C");
            }
            
        }
        private void Volume_Up_Click(object sender, System.Windows.Input.MouseEventArgs e)            //音量增加
        {
            Client.SendCommand(@"10|U");
        }

        private void Mute_Click(object sender, System.Windows.Input.MouseEventArgs e)                     //静音
        {
            Client.SendCommand(@"10|A");
        }

        private void Transport_Rew_Click(object sender, System.Windows.Input.MouseEventArgs e)              //快退
        {
            Client.SendCommand(@"10|L");
        }

        private void Play_Pause_Click(object sender, System.Windows.Input.MouseEventArgs e)                      //开始/暂停
        {
            Client.SendCommand(@"10|P");
        }

        private void Transport_Ff_Click(object sender, System.Windows.Input.MouseEventArgs e)                       //快进
        {
            Client.SendCommand(@"10|R");
        }

        private void The_Last_Section_Click(object sender, System.Windows.Input.MouseEventArgs e)                 //上一节
        {
            Client.SendCommand(@"10|F");
        }

        private void Volume_Down_Click(object sender, System.Windows.Input.MouseEventArgs e)                //音量减小
        {
            Client.SendCommand(@"10|D");
        }

        private void The_Next_Section_Click(object sender, System.Windows.Input.MouseEventArgs e)           //下一节
        {
            Client.SendCommand(@"10|B");
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            if(value == 0)
            {
                value = 1;
                this.sign.Content = new Now_Key("关闭手势");
                timer.Start();
            }
            else
            {
                value = 0;
                this.sign.Content = new Now_Key("打开手势");
                timer.Stop();
            }
        }

        private void OnUpdate(object sender, GameTimerEventArgs e)                                      //发送指令
        {
            Debug.WriteLine(accHelper.GetInfo(), DateTime.Now.ToShortTimeString());
            if (accHelper.GetInfo() == "UpShake" && Control == 1)
            {
                Control = 0;
                Client.SendCommand(@"10|U");
            }
            else if (accHelper.GetInfo() == "DownShake" && Control == 1)
            {
                Control = 0;
                Client.SendCommand(@"10|D");
            }
            else if (accHelper.GetInfo() == "LeftShake" && Control == 1)
            {
                Control = 0;
                Client.SendCommand(@"10|F");
            }
            else if (accHelper.GetInfo() == "RightShake" && Control == 1)
            {
                Control = 0;
                Client.SendCommand(@"10|B");
            }
            else if (accHelper.GetInfo() == "None")
            {
                Control = 1;
            }
        }
    }
}