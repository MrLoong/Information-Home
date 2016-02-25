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
    public partial class Welcome : PhoneApplicationPage
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ComeIn.xaml", UriKind.Relative));
        }
    }
}