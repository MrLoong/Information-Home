using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Information_Home.Resources;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Threading;

namespace Information_Home
{
    public partial class Push_The_Screen : PhoneApplicationPage
    {
        WP_Client Client = null;
        Thread threadScreen;
        public Push_The_Screen()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            threadScreen = new Thread(new ThreadStart(Show_Image));
            base.OnNavigatedTo(e);
            Client = Connect.Client;
            threadScreen.Start();
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            threadScreen.Abort();
            base.OnNavigatedFrom(e);
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
             NavigationService.GoBack();
        }
        private void Show_Image()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(380);
                Dispatcher.BeginInvoke((ThreadStart)delegate()
                {
                    Client.SendCommand(@"size?12|"); //发送指令来回收大小
                    //int size = Client.Receive(@"size?12|");                                   //接收大小
                    int size = 60000;
                    //System.Threading.Thread.Sleep(200);
                    Debug.WriteLine("一百" + size.ToString(), DateTime.Now.ToShortTimeString());
                    //Client.SendCommand(@"12|", size);                                     //发送指令来回收数据
                    byte[] re_str = Client.Receive(size, size);                                   //接收数据
                    BitmapImage cutImage = ChangeImageWithByte.ByteArrayToBitmapImage(re_str);
                    this.ShowImage.Source = cutImage;
                });
            }
        }
    }
}