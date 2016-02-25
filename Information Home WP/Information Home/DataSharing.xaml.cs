using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System.IO;
using System.Windows.Media.Imaging;
using System.Text;
using System.IO.IsolatedStorage;
using System.ServiceModel.Channels;
using System.Threading;

namespace Information_Home
{
    public partial class DataSharing : PhoneApplicationPage
    {
        WP_Client Client = null;
        public DataSharing()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add("*");
            picker.PickSingleFileAndContinue();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ConPPT.IsEnabled = false;
            Client = Connect.Client;
            changlanguage();
            Client.SendCommand("data?");
            if (e.NavigationMode == NavigationMode.Back && PickFiles.PickedFiles.Count > 0)
            {
                StorageFile file = PickFiles.PickedFiles[0];
                IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                Stream stream = WindowsRuntimeStreamExtensions.AsStreamForRead(fileStream.GetInputStreamAt(0));
                byte[] bt = ConvertStreamTobyte(stream);
                
                Client.SendCommand("FileName|" + file.Name);
                Thread.Sleep(1000);
                Client.SendCommand(bt);

                string[] type = file.Name.Split('.');
                if (type[type.Length - 1] == "ppt")
                {
                    this.ConPPT.IsEnabled = true;
                }
            }
            
        }
        void changlanguage()
        {
            if (Information.Language == 0)
            {
                this.shujufengxiang.Text = "数据分享";
                this.shujufengxiang.FontSize = 75;
                this.xuanzhewenjian.Content = "选择文件";
                this.xuanzhewenjian.FontSize = 45;
                this.ConPPT.Content = "控制PPT";
                this.ConPPT.FontSize = 30;
            }
            else if (Information.Language == 1)
            {
                this.shujufengxiang.Text = "Data-sharing";
                this.shujufengxiang.FontSize = 50;
                this.xuanzhewenjian.Content = "Select the file";
                this.xuanzhewenjian.FontSize = 30;
                this.ConPPT.Content = "Control PPT";
                this.ConPPT.FontSize = 25;
            }
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.ConPPT.IsEnabled = false;
            string stop = "|||***|||";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(stop.ToString());
            Client.SendCommand(buffer);
            base.OnNavigatedFrom(e);
        }  

        public static byte[] ConvertStreamTobyte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void Con_Ppt_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Control_PPT.xaml", UriKind.Relative));
        }
        private void Go_Back_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}