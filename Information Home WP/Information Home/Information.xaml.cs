using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Windows.Phone.UI.Input;
using System.Diagnostics;
using Coding4Fun.Toolkit.Controls;
using Windows.Storage;
using System.IO;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace Information_Home
{
    public partial class Information : PhoneApplicationPage
    {
        WP_Client Client = null;
        public static int goTo = 0;
        public static int Language =0;
        public Information()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            goTo = 0;
            base.OnNavigatedTo(e);
            Client = Connect.Client;
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (goTo == 0)
            {
                Client.Leave_Out();
                goTo = 0;
            }
            base.OnNavigatedFrom(e);
        }

        private void Document_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("文件获取", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Document.xaml", UriKind.Relative));
        }

        private void Video_Control_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("视频控制", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Video_Remote_Control.xaml", UriKind.Relative));
        }

        private void Push_The_Screen(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("屏幕推送", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Push_The_Screen.xaml", UriKind.Relative));
        }

        private void Scenic_Sharing_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("风景共享", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Scenic_Sharing.xaml", UriKind.Relative));
        }

        private void Voice_Calls_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("语音通话", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Voice_Calls.xaml", UriKind.Relative));
        }

        private void Data_Sharing_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("文件分享", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/DataSharing.xaml", UriKind.Relative));
        }

        private void Con_Ppt_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("控制ppt", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Control_PPT.xaml", UriKind.Relative));
        }

        private void Easy_Use_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("轻松使用", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Easy_Use.xaml", UriKind.Relative));
        }

        private void System_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("系统管理", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/System_Manage.xaml", UriKind.Relative));
        }

        private void Key_Box_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("模拟键盘", DateTime.Now.ToShortTimeString());
            goTo = 1;
            this.NavigationService.Navigate(new Uri("/Key_Box.xaml", UriKind.Relative));
        }

        private void Change_Language(object sender, RoutedEventArgs e)
        {
            if(Information.Language == 0)
            {
                Information.Language = 1;
                this.gongneng.Text = "Function";
                this.gongneng.FontSize = 40;
                this.zhongyingqiehuan.Content = "Chinese";
                this.zhongyingqiehuan.FontSize = 35;
                this.wenjian.Text = "Get";
                this.wenjian.FontSize = 35;
                this.huoqu.Text = "file";
                this.huoqu.FontSize = 35;
                this.shiping.Text = "Video";
                this.shiping.FontSize = 35;
                this.yaokong.Text = "control";
                this.yaokong.FontSize = 32;
                this.pingmu.Text = "Screen";
                this.pingmu.FontSize = 35;
                this.tuisong.Text = "push";
                this.tuisong.FontSize = 35;
                this.fenjing.Text = "Landscape";
                this.fenjing.FontSize = 24;
                this.gongxiang.Text = "sharing";
                this.gongxiang.FontSize = 35;
                this.yuying.Text = "Voice";
                this.yuying.FontSize = 35;
                this.tonghua.Text = "call";
                this.tonghua.FontSize = 35;
                this.shuju.Text = "Data";
                this.shuju.FontSize = 35;
                this.fengxiang.Text = "sharing";
                this.fengxiang.FontSize = 35;
                this.kongzhi.Text = "Control";
                this.kongzhi.FontSize = 35;
                this.PPT.Text = "PPT";
                this.PPT.FontSize = 35;
                this.moni.Text = "Simulation";
                this.moni.FontSize = 22;
                this.jianpan.Text = "keyboard";
                this.jianpan.FontSize = 28;
                this.qingsong.Text = "Easy";
                this.qingsong.FontSize = 35;
                this.shiyong.Text = "use";
                this.shiyong.FontSize = 35;
                this.xitong.Text = "System";
                this.xitong.FontSize = 35;
                this.guanli.Text = "management";
                this.guanli.FontSize = 18;
            }
            else if(Information.Language == 1)
            {
                Information.Language = 0;
                this.gongneng.Text = "功能";
                this.gongneng.FontSize = 45;
                this.zhongyingqiehuan.Content = "英文";
                this.zhongyingqiehuan.FontSize = 35;
                this.wenjian.Text = "文件";
                this.wenjian.FontSize = 45;
                this.huoqu.Text = "获取";
                this.huoqu.FontSize = 45;
                this.shiping.Text = "视频";
                this.shiping.FontSize = 45;
                this.yaokong.Text = "遥控";
                this.yaokong.FontSize = 45;
                this.pingmu.Text = "屏幕";
                this.pingmu.FontSize = 45;
                this.tuisong.Text = "推送";
                this.tuisong.FontSize = 45;
                this.fenjing.Text = "风景";
                this.fenjing.FontSize = 45;
                this.gongxiang.Text = "共享";
                this.gongxiang.FontSize = 45;
                this.yuying.Text = "语音";
                this.yuying.FontSize = 45;
                this.tonghua.Text = "通话";
                this.tonghua.FontSize = 45;
                this.shuju.Text = "数据";
                this.shuju.FontSize = 45;
                this.fengxiang.Text = "分享";
                this.fengxiang.FontSize = 45;
                this.kongzhi.Text = "控制";
                this.kongzhi.FontSize = 45;
                this.PPT.Text = "PPT";
                this.PPT.FontSize = 45;
                this.moni.Text = "模拟";
                this.moni.FontSize = 45;
                this.jianpan.Text = "键盘";
                this.jianpan.FontSize = 45;
                this.qingsong.Text = "轻松";
                this.qingsong.FontSize = 45;
                this.shiyong.Text = "使用";
                this.shiyong.FontSize = 45;
                this.xitong.Text = "系统";
                this.xitong.FontSize = 45;
                this.guanli.Text = "管理";
                this.guanli.FontSize = 45;
            }
        }
    }
}