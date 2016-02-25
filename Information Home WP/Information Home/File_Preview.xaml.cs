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
    public partial class File_Preview : PhoneApplicationPage
    {
        string top_nmae;
        string txt;
        public File_Preview()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.NavigationContext.QueryString.TryGetValue("top", out top_nmae);
            this.NavigationContext.QueryString.TryGetValue("doc", out txt);
            this.top_Name.DataContext = new Now_Top(top_nmae);
            this.File_Text.DataContext = new Now_Top(txt);
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}