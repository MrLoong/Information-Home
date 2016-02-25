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
    public partial class ComeIn : PhoneApplicationPage
    {
        public ComeIn()
        {
            InitializeComponent();
        }

        private void Connent_Hx(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Connect.xaml?ip=" + this.ZhuIp.Text.ToString(), UriKind.Relative));
        }

        private void Me_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Connect.xaml?ip=null", UriKind.Relative));
        }
    }
}