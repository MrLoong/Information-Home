using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Information_Home
{
    public partial class Easy_Use : PhoneApplicationPage
    {
        WP_Client Client = null;
        public Easy_Use()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Client = Connect.Client;
            changlanguage();
        }
        void changlanguage()
        {
            if (Information.Language == 0)
            {
                this.yuyingshibie.Content = "语音识别";
                this.yuyingshibie.FontSize = 30;
                this.fangdajing.Content = "放大镜";
                this.fangdajing.FontSize = 30;
                this.jiangshuren.Content = "讲述人";
                this.jiangshuren.FontSize = 30;
                this.pingmujianpang.Content = "屏幕键盘";
                this.pingmujianpang.FontSize = 30;
            }
            else if (Information.Language == 1)
            {
                this.yuyingshibie.Content = "Speech recognition";
                this.yuyingshibie.FontSize = 15;
                this.fangdajing.Content = "The magnifying glass";
                this.fangdajing.FontSize = 15;
                this.jiangshuren.Content = "Narrator";
                this.jiangshuren.FontSize = 15;
                this.pingmujianpang.Content = "The on-screen keyboard";
                this.pingmujianpang.FontSize = 13;
            }
        }

        private void Voice_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|YYSB");
        }

        private void Big_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|FDJ");
        }

        private void Speak_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|JSR");
        }

        private void Key_Box_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|PMJP");
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}