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

namespace Information_Home
{
    public partial class Control_PPT : PhoneApplicationPage
    {
        GameTimer timer;
        WPAccHelper accHelper;
        WP_Client Client = null;
        int key = 0;
        int value = 0;
        int Control = 0;
        public Control_PPT()
        {
            InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            changelanguage();
            Client = Connect.Client;
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            accHelper = new WPAccHelper();
            accHelper.SupportedAction = ActionEnum.LeftShake | ActionEnum.UpShake | ActionEnum.RightShake | ActionEnum.DownShake;
        }
        void changelanguage()
        {
            if (Information.Language == 0)
            {
                this.xiayizhang.Text = "下一张幻灯片";
                this.shangyizhang.Text = "上一张幻灯片";
                this.kongbai.Text = "空白";
                this.kongbai.FontSize = 25;
                this.kaishihuandengping.Text = "开始幻灯片";
                this.tingzhihuangddengping.Text = "停止幻灯片";
                this.shoushi.Content = "打开手势";
                this.shoushi.FontSize = 25;
                this.tuichuyangjiang.Content = "退出演讲";
                this.tuichuyangjiang.FontSize = 25;
            }
            else if(Information.Language == 1)
            {
                this.xiayizhang.Text="Next page";
                this.shangyizhang.Text="Previous page";
                this.kongbai.Text="New File";
                this.kongbai.FontSize = 13;
                this.kaishihuandengping.Text="Start";
                this.tingzhihuangddengping.Text="Stop";
                this.shoushi.Content = "Hand gestures";
                this.shoushi.FontSize = 15;
                this.tuichuyangjiang.Content = "Exit presentation";
                this.tuichuyangjiang.FontSize = 15;
            }
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)//结束界面
        {
            key = 0;
            value = 0;
            Control = 0;
            timer.Update -= OnUpdate;
            base.OnNavigatedFrom(e);
        }

        private void Play_Next_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"13|D");
        }

        private void Play_Last_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"13|U");
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"13|N");
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"13|F");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"13|E");
        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void OnUpdate(object sender, GameTimerEventArgs e)                                      //发送指令
        {
            Debug.WriteLine(accHelper.GetInfo(), DateTime.Now.ToShortTimeString());
            if (accHelper.GetInfo() == "LeftShake" && Control == 1)
            {
                Control = 0;
                Client.SendCommand(@"13|U");
            }
            else if (accHelper.GetInfo() == "RightShake" && Control == 1)
            {
                Control = 0;
                Client.SendCommand(@"13|D");
            }
            else if (accHelper.GetInfo() == "None")
            {
                Control = 1;
            }
        }

        private void Shou_Click(object sender, RoutedEventArgs e)
        {
            if(Information.Language==0){
                if (value == 0)
                {
                    value = 1;
                    this.shoushi.Content = new Now_Key("关闭手势");
                    timer.Start();
                }
                else
                {
                    value = 0;
                    this.shoushi.Content = new Now_Key("打开手势");
                    timer.Stop();
                }
            }
            
        }
    }
}