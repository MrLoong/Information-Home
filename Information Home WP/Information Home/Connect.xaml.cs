using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Information_Home.Hx_server;
class Ip_Item
{
    public int key;
    public string Name { get; set; }//文件名
    public string Ipdata { get; set; }
}

namespace Information_Home
{

    public partial class Connect : PhoneApplicationPage
    {
        public static WP_Client Client = new WP_Client();
        public static string Ipstring = "";
        private List<Ip_Item> Ip_Item;
        public Thread threadIP; 
        public Thread threadIP2;
        public WP_HX_Client wp_hx_Client = new WP_HX_Client();
        public static StringBuilder NowIp = new StringBuilder();
        string ip;
        public Connect()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)      //wp开始界面初始化
        {
            base.OnNavigatedTo(e);
            this.NavigationContext.QueryString.TryGetValue("ip", out ip);
            if(ip!="null")
            {
                if (wp_hx_Client.Client_HX(ip))
                {
                    wp_hx_Client.Send_list("CONN|longshao7");
                    threadIP = new Thread(new ThreadStart(Bind_Data));
                    threadIP.Start();
                    threadIP2 = new Thread(new ThreadStart(Bind_Data_List));
                    threadIP2.Start();
                }
                else
                {
                    MessageBox.Show("大厅未开启！But，仍然可以被手动连接PC端。");
                }
            }
            else
            {
                MessageBox.Show("大厅未连接！But，仍然可以被手动连接PC端。");
            }
            
        }
        void Bind_Data_List()             //ListBox绑定数据显示
        {
            while (true)
            {
                Thread.Sleep(2000);
                Ip_Item = new List<Ip_Item>(); //初始化
                string nowAll = NowIp.ToString();
                string[] ips = nowAll.Split('|');
                for (int i = 0; i < ips.Length - 1; i++)
                    {
                        string[] file = ips[i].Split('$');
                        Ip_Item.Add(new Ip_Item() { Ipdata = file[1] });
                    }
                Dispatcher.BeginInvoke((ThreadStart)delegate()
                {

                    this.List_Documents.ItemsSource = Ip_Item;//指定ListBox的数据来源为的数组
                });
            }
            
            
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)//结束界面
        {
            NowIp.Length = 0;
            GetHostIPAddress hostIP = new GetHostIPAddress();
            string ip = hostIP.GetIPAddress();
            wp_hx_Client.Send_list("Signout|" + ip);
            wp_hx_Client.Leave_Out();                      //与主服务器断开连接
            base.OnNavigatedFrom(e);
        }

        private void onConnect(object sender, RoutedEventArgs e)
        {
            Ipstring = this.txtServerIP.Text;
            wp_hx_Client.Leave_Out();                      //与主服务器断开连接
            Boolean key = Client.Connect(this.txtServerIP.Text);
            if (key)
            {
                Debug.WriteLine("wwwwwwwwww", DateTime.Now.ToLongTimeString());
                //Dispatcher.BeginInvoke(() => this.NavigationService.Navigate(new Uri("/Information.xaml", UriKind.Relative)));
                this.NavigationService.Navigate(new Uri("/Information.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("连接失败！！！");
                Client.Leave_Out();
            }

        }

        void Bind_Data()                                                                    //ListBox绑定数据显示
        {

            try
            {
                List<information> List_User = new List<information>();
                information l_LAVE = new information();
                while (true)
                {
                    string[] tokens = wp_hx_Client.Re_List().Split(new Char[] { '|' });
                    if (tokens[0] == "Client")
                    {
                        try
                        {
                            List_User = (List<information>)FromJson(tokens[1]);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        foreach (information p in List_User)
                        {
                            //MessageBox.Show("用户登录"+p.name + "  " + p.ip);
                            NowIp.Append(p.name + "$" + p.ip + "|");
                        }
                        List_User.Clear();
                    }
                    else if (tokens[0] == "Quit")
                    {
                        try
                        {
                            l_LAVE = (information)FromJson_leave(tokens[1]);

                            //MessageBox.Show("用户退出" + l_LAVE.name + "   " + l_LAVE.ip);
                            Debug.WriteLine("用户退出" + l_LAVE.name + "   " + l_LAVE.ip, DateTime.Now.ToLongTimeString());
                            string nowAll = NowIp.ToString();
                            string[] item = nowAll.Split('|');
                            NowIp.Length = 0;
                            for (int i = 0; i < item.Length-1; i++)
                            {
                                string[] p = item[i].Split('$');
                                if (p[1] != l_LAVE.ip)
                                {
                                    NowIp.Append(item[i]+"|");
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message, DateTime.Now.ToLongTimeString());
                        }
                        List_User.Clear();
                        //List_Documents.ItemsSource = Ip_Item;//指定ListBox的数据来源为的数组
                    }
                }
            }
            catch (Exception e)
            {

            }

        }
        public information FromJson_leave(string json)
        {
            information jsonObject = null;
            try
            {

                var ser = new DataContractJsonSerializer(typeof(information));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(information));
                    jsonObject = (information)jsonSerializer.ReadObject(ms);
                }


            }
            catch (Exception e)
            {

            }
            return jsonObject;
        }
        public List<information> FromJson(string json)
        {
            //MessageBox.Show(json);
            List<information> jsonObject = null;
            var ser = new DataContractJsonSerializer(typeof(information));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<information>));
                jsonObject = (List<information>)jsonSerializer.ReadObject(ms);
            }
            return jsonObject;
        }

        private void List_Documents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var lbi in List_Documents.SelectedItems)
            {
                if (lbi != null)
                {
                    
                    Ipstring = ((lbi as Ip_Item).Ipdata).ToString();
                    Boolean key = Client.Connect(Ipstring);
                    if (key)
                    {
                        this.NavigationService.Navigate(new Uri("/Information.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("连接失败！！！");
                    }
                }
            }
        }
    }
}