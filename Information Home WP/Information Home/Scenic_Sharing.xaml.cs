using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Windows.Media;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Media.Imaging;
using Windows.Phone.Media.Capture;
using System.Threading;
using System.Diagnostics;

namespace Information_Home
{
    public partial class Scenic_Sharing : PhoneApplicationPage
    {
        private CaptureSource captureSource;
        private VideoCaptureDevice videoCaptureDevice;
        WP_Client Client = null;
        Thread threadShare;
        Thread threadInvite;                        //邀请按钮线程
        private int key = 0; 
        public Scenic_Sharing()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Client = Connect.Client;
            changlanguage();
            threadInvite = new Thread(Invite);                            //实例化线程
            threadShare = new Thread(new ThreadStart(GetPreview));
            if (captureSource == null)
            {
                // 创建摄像机对象。
                captureSource = new CaptureSource();
                videoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
                // eventhandlers capturesource添加为。
                captureSource.CaptureFailed += new EventHandler<ExceptionRoutedEventArgs>(OnCaptureFailed);
                // 初始化相机如果存在手机上。
                if (videoCaptureDevice != null)
                {
                    TheVideoBrush.SetSource(captureSource);
                    captureSource.Start();
                }
                else
                {
                    MessageBox.Show("您的摄像头设备不支持");
                }
            }
        }
        void changlanguage()
        {
            if (Information.Language == 0)
            {
                this.Share.Content = "邀请分享";
                this.closeShare.Content = "关闭分享";
            }
            else if (Information.Language == 1)
            {
                this.Share.Content = "Invite ";
                this.closeShare.Content = "Close";
            }
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            Debug.WriteLine("3333string", DateTime.Now.ToShortTimeString());
            if (key == 1)
            {
                threadShare.Abort();
                Debug.WriteLine("2222string", DateTime.Now.ToShortTimeString());
                string stop = "|||***|||";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(stop.ToString());
               Client.SendCommand(buffer);
            }
            base.OnNavigatedFrom(e);
        }  
        private void OnCaptureFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(delegate()
            {
                MessageBox.Show("ERROR: " + e.ErrorException.Message.ToString());
            });
        }
        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            // 把下面的代码注释掉，页面的方向就会锁定 
            base.OnOrientationChanged(e); 
        }
        private void GetPreview()
        {
            while (key == 1)
            {
                System.Threading.Thread.Sleep(200);
                Dispatcher.BeginInvoke((ThreadStart) delegate(){
                    WriteableBitmap wBitmap = new WriteableBitmap(640, 480);
                    wBitmap.Render(Re_video, new MatrixTransform());//截取视频
                    wBitmap.Invalidate();
                    MemoryStream ms = new MemoryStream();
                    wBitmap.SaveJpeg(ms, wBitmap.PixelWidth, wBitmap.PixelHeight, 0, 100);
                    byte[] buffer = new byte[ms.Length];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(buffer, 0, (int)ms.Length);
                   Client.SendCommand(buffer);
                });
            }
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            threadShare.IsBackground = true;
            threadShare.Start();
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (!this.closeShare.IsEnabled)
            {
                e.Cancel = true;
            }
            else
            {
                NavigationService.GoBack();
            }
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("1111string", DateTime.Now.ToShortTimeString());
            NavigationService.GoBack();
        }

        private void Invite_Click(object sender, RoutedEventArgs e)
        {
            threadInvite = new Thread(Invite);                            //实例化线程
            threadInvite.IsBackground = true;
            threadInvite.Start();
            this.Share.IsEnabled = false;
            this.closeShare.IsEnabled = false;
        }

        private void Invite()
        {
            Client.SendCommand("play?");
            string rec = Client.Receive(0);Debug.WriteLine("string" + rec, DateTime.Now.ToShortTimeString());
            Dispatcher.BeginInvoke((ThreadStart) delegate(){
                if (rec == "OK")
                {
                    key = 1;
                    threadShare.IsBackground = true;
                    threadShare.Start();
                    threadInvite.Abort();
                    this.closeShare.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("对方拒绝！");
                    threadInvite.Abort();
                    this.Share.IsEnabled = true;
                    this.closeShare.IsEnabled = true;
                }
            });
        }
    }
}