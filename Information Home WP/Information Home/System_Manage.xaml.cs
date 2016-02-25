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
    public partial class System_Manage : PhoneApplicationPage
    {
        private WP_Client Client = null;
        public System_Manage()
        {
            InitializeComponent();
            this.Client = Connect.Client;
            changlanguage();
        }
        void changlanguage()
        {
            if (Information.Language == 0)
            {
                this.jieshu.Content = "结束任务";
                this.jieshu.FontSize = 30;
                this.shuding.Content = "锁定计算机";
                this.shuding.FontSize = 30;
                this.guanji.Content = "关机";
                this.guanji.FontSize = 30;
                this.chongqi.Content = "重启";
                this.chongqi.FontSize = 30;
            }
            else if (Information.Language == 1)
            {
                this.jieshu.Content = "End task";
                this.jieshu.FontSize = 30;
                this.shuding.Content = "Lock computer";
                this.shuding.FontSize = 25;
                this.guanji.Content = "Shutdown";
                this.guanji.FontSize = 30;
                this.chongqi.Content = "Restart";
                this.chongqi.FontSize = 30;
            }
        }
        private void CQ_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|CQ");
        }

        private void GJ_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|GJ");
        }

        private void SDJSJ_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|SDJSJ");
        }

        private void JSRW_Click(object sender, RoutedEventArgs e)
        {
            Client.SendCommand(@"14|JSRW");
        }
    }
}