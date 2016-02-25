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
using System;

class Documents_Item
{
    public int key;
    public string Name { get; set; }//文件名
    public int size { get; set; }
    public string ImageUrl { get; set; }
}

class Now_Top                            //头名字
{
    public string now_way { get; set; }
    public Now_Top(string now)
    {
        now_way = now;
    }
    // Override the ToString method.
    public override string ToString()
    {
        return now_way;
    }
}

namespace Information_Home
{
    public partial class Document : PhoneApplicationPage
    {
        WP_Client Client = null;
        private List<Documents_Item> Documents_Items;
        private const string imageUrl = "/images/";
        public static string nowWay = "";         //当前路径
        public static string lastWay = "";         //上一级路径
        public static string nowName = "";           //当前文件名
        int value = 0;
        int isSelected = 0;                            //判断操作
        string pitch_on_name = "";                         //选中的名字
        string pitch_on_image = "";                         //选中的图片
        public static int goToto = 0;
        public Document()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (goToto == 0)
            {
                nowWay = "";         //当前路径
                lastWay = "";         //上一级路径
                nowName = "";           //当前文件名
                goToto = 0;
            }
            base.OnNavigatedFrom(e);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Client = Connect.Client;
            if (goToto == 0)
            {
                Debug.WriteLine(goToto + "+11111111" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                Client.SendCommand(@"size?0|");                                        //发送指令来回收大小
                int size = Client.Receive(@"size?0|");                                   //接收大小
                Client.SendCommand(@"0|", size);                                     //发送指令来回收数据
                string re_str = Client.Receive(size);                                   //接收数据
                Debug.WriteLine("第一" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                Documents_Items = new List<Documents_Item>(); //初始化
                string[] device = re_str.Split('|');
                for (int i = 0; i < device.Length - 1; i++)
                {
                    Documents_Items.Add(new Documents_Item() { Name = device[i], size = 40, ImageUrl = imageUrl + "Hard_Disk.png" });
                }
                this.top_Text.DataContext = new Now_Top("我的电脑");
                List_Documents.ItemsSource = Documents_Items;//指定ListBox的数据来源为的数组
            }
            else if (goToto == 1)
            {
                goToto = 0;
                Debug.WriteLine(goToto + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
                int size = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
                Client.SendCommand(@"1|" + nowWay, size);                                     //发送指令来回收数据
                string re_str = Client.Receive(size);                                   //接收数据
                Debug.WriteLine("第二" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                this.top_Text.DataContext = new Now_Top(nowName);
                Bind_Data(re_str);
            }
        }
        void Bind_Data(string str)                                                                    //ListBox绑定数据显示
        {
            Documents_Items = new List<Documents_Item>(); //初始化
            string[] files = str.Split('|');
            for (int i = 0; i < files.Length - 1; i++)
            {
                string[] file = files[i].Split('*');
                if (file[1] == "0")
                {
                    Documents_Items.Add(new Documents_Item() { Name = file[0], size = 40, ImageUrl = imageUrl + "File.png" });
                }
                else if (file[1] == "1")
                {
                    Documents_Items.Add(new Documents_Item() { Name = file[0], size = 40, ImageUrl = imageUrl + "Folder.png" });
                }
            }
            List_Documents.ItemsSource = Documents_Items;//指定ListBox的数据来源为的数组
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)             //物理返回键
        {
            NavigationService.GoBack();
        }
        private void Go_Back_Click(object sender, EventArgs e)                               //虚拟返回键
        {
            if (nowWay == "")
            {
                NavigationService.GoBack();                                                    //返回上一页面
                return;
            }
            if (lastWay == "")
            {
                nowWay = "";
                nowName = "";
                Client.SendCommand(@"size?0|");                                        //发送指令来回收大小
                int size = Client.Receive(@"size?0|");                                   //接收大小
                Client.SendCommand(@"0|", size);                                     //发送指令来回收数据
                string re_str = Client.Receive(size);                                   //接收数据
                Debug.WriteLine("第三" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                Documents_Items = new List<Documents_Item>(); //初始化
                string[] device = re_str.Split('|');
                for (int i = 0; i < device.Length - 1; i++)
                {
                    Documents_Items.Add(new Documents_Item() { Name = device[i], size = 40, ImageUrl = imageUrl + "Hard_Disk.png" });
                }
                this.top_Text.DataContext = new Now_Top("我的电脑");
                List_Documents.ItemsSource = Documents_Items;//指定ListBox的数据来源为的数组
                return;
            }
            Debug.WriteLine("fanhui+" + nowWay + "+" + lastWay + "+" + nowName, DateTime.Now.ToShortTimeString());
            nowWay = lastWay;
            string[] name = nowWay.Split('\\');
            nowName = name[name.Length - 2];
            if (nowName == @"C:" || nowName == @"D:" || nowName == @"E:" || nowName == @"F:" || nowName == @"G:" || nowName == @"H:" || nowName == @"I:")
            {
                nowName += @"\";
            }
            if (name.Length == 2)
            {
                lastWay = "";
            }
            else
            {
                lastWay = this.Get_Last(nowWay, name[name.Length - 2] + "\\");
            }
            Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
            int size1 = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
            Client.SendCommand(@"1|" + nowWay, size1);                                     //发送指令来回收数据
            string re_str1 = Client.Receive(size1);                                   //接收数据
            Debug.WriteLine("第四" + re_str1 + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
            this.top_Text.DataContext = new Now_Top(nowName);
            Bind_Data(re_str1);
        }
        string Get_Last(string a, string b)                              //字符串相减
        {
            char[] chara = a.ToCharArray();
            char[] charb = b.ToCharArray();
            for (int i = 1; i <= charb.Length; i++)
            {
                if (chara[chara.Length - i] == charb[charb.Length - i])
                {
                    chara[chara.Length - i] = ' ';
                }
            }
            string newstr = new string(chara);
            newstr = newstr.Replace(" ", "");
            return newstr;
        }
        private void New_Folder_Click(object sender, EventArgs e)                      //新建文件夹           
        {
            var input_Box = new InputPrompt
            {
                Title = "新建文件夹",
                Message = "请输入文件夹名称",
            };
            input_Box.Completed += input;
            input_Box.Show();
        }
        void input(object sender, PopUpEventArgs<string, PopUpResult> e)                         //新建文件夹   
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                int i = 0;
                Client.SendCommand(@"6|" + nowWay + e.Result, i);                                     //发送指令来回收数据,但不接收

                Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
                int size = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
                Client.SendCommand(@"1|" + nowWay, size);                                     //发送指令来回收数据
                string re_str = Client.Receive(size);                                   //接收数据
                Debug.WriteLine("第五" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                this.top_Text.DataContext = new Now_Top(nowName);
                Bind_Data(re_str);
            }
            else if (e.PopUpResult == PopUpResult.Cancelled)
            {
                //MessageBox.Show("CANCELLED! " + e.Result);
            }
        }
        private void Search_Click(object sender, EventArgs e)                                     //搜索文件 
        {
            var input_Box = new InputPrompt
            {
                Title = "搜索文件",
                Message = "请输入文件名称",
            };
            input_Box.Completed += Search;
            input_Box.Show();
        }
        void Search(object sender, PopUpEventArgs<string, PopUpResult> e)                         //搜索文件 
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                Debug.WriteLine("02320" + @"2|" + nowWay + "|" + e.Result, DateTime.Now.ToShortTimeString());
                //搜索直接固定大小接收
                //Client.SendCommand(@"size?2|" + nowWay + "|" + e.Result);                                        //发送指令来回收大小
                //int size = Client.Receive(@"size?2|" + nowWay + "|" + e.Result);                                   //接收大小
                int size = 10240;
                Client.SendCommand(@"2|" + nowWay + "|" + e.Result, size);                                     //发送指令来回收数据
                string re_str = Client.Receive(size);                                   //接收数据
                Debug.WriteLine("第六" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                if (re_str == "|||***|||")
                {
                    Documents_Items = new List<Documents_Item>(); //初始化
                    Documents_Items.Add(new Documents_Item() { Name = "无", size = 40, ImageUrl = imageUrl + "File.png" });
                    this.top_Text.DataContext = new Now_Top("目标文件");
                    List_Documents.ItemsSource = Documents_Items;//指定ListBox的数据来源为的数组
                }
                else
                {
                    Debug.WriteLine("第七" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                    Documents_Items = new List<Documents_Item>(); //初始化
                    string[] ways = re_str.Split('|');
                    for (int i = 0; i < ways.Length - 1; i++)
                    {
                        string[] way = ways[i].Split('\\');
                        Documents_Items.Add(new Documents_Item() { Name = way[way.Length - 1] + "\n" + ways[i], size = 30, ImageUrl = imageUrl + "File.png" });
                    }
                    this.top_Text.DataContext = new Now_Top("目标文件");
                    List_Documents.ItemsSource = Documents_Items;//指定ListBox的数据来源为的数组
                }
            }
            else if (e.PopUpResult == PopUpResult.Cancelled)
            {
                //MessageBox.Show("CANCELLED! " + e.Result);
            }
        }
        private void Rename_Click(object sender, RoutedEventArgs e)                                 //重命名
        {
            if (this.top_Text.DataContext.ToString() != "目标文件")
            {
                isSelected = 1;
                ListBoxItem selectedListBoxItem = this.List_Documents.ItemContainerGenerator.ContainerFromItem((sender as MenuItem).DataContext) as ListBoxItem;
                selectedListBoxItem.IsSelected = true;                                           //让listBox列表处于选中状态
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)                                     //保存文件
        {
            if (this.top_Text.DataContext.ToString() != "目标文件")
            {
                isSelected = 2;
                ListBoxItem selectedListBoxItem = this.List_Documents.ItemContainerGenerator.ContainerFromItem((sender as MenuItem).DataContext) as ListBoxItem;
                selectedListBoxItem.IsSelected = true;                                           //让listBox列表处于选中状态
            }                                           //让listBox列表处于选中状态
        }

        private void Delete_Click(object sender, RoutedEventArgs e)                                 //删除文件
        {
            if (this.top_Text.DataContext.ToString() != "目标文件")
            {
                isSelected = 3;
                ListBoxItem selectedListBoxItem = this.List_Documents.ItemContainerGenerator.ContainerFromItem((sender as MenuItem).DataContext) as ListBoxItem;
                selectedListBoxItem.IsSelected = true;                                           //让listBox列表处于选中状态
            }                                       //让listBox列表处于选中状态
        }
        private void List_Documents_SelectionChanged(object sender, SelectionChangedEventArgs e)                                  //选中ListBox项
        {
            if (isSelected == 0)                                                            //直接选中状态
            {
                string file_way = "";
                string name = "";
                if (this.top_Text.DataContext.ToString() != "目标文件")
                {
                    isSelected = 0;

                    foreach (var lbi in List_Documents.SelectedItems)
                    {
                        if (lbi != null)
                        {
                            string image = ((lbi as Documents_Item).ImageUrl).ToString();
                            name = ((lbi as Documents_Item).Name).ToString();

                            if (image == "/images/Hard_Disk.png" || image == "/images/Folder.png")
                            {
                                nowName = name;
                                value = 1;
                                nowWay += nowName;
                                lastWay = Get_Last(nowWay, nowName);
                                if (nowWay != @"C:\" && nowWay != @"D:\" && nowWay != @"E:\" && nowWay != @"F:\" && nowWay != @"G:\" && nowWay != @"H:\" && nowWay != @"I:\")
                                {
                                    nowWay += @"\";
                                }
                            }
                            else
                            {
                                value = 2;
                                file_way = nowWay;
                                file_way += name;
                            }
                        }
                    }
                }
                if (value == 1)
                {
                    value = 0;
                    Debug.WriteLine(nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());

                    Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
                    int size = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
                    Client.SendCommand(@"1|" + nowWay, size);                                     //发送指令来回收数据
                    string re_str = Client.Receive(size);                                   //接收数据
                    Debug.WriteLine("第八" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                    if (re_str == "|||***|||") { re_str = ""; }
                    Debug.WriteLine("0000" + re_str, DateTime.Now.ToShortTimeString());
                    this.top_Text.DataContext = new Now_Top(nowName);
                    Bind_Data(re_str);
                }
                else if (value == 2)
                {
                    value = 0;
                    string[] type = name.Split('.');
                    if (type[type.Length - 1] == "txt")                   //txt文件预览
                    {

                        Client.SendCommand(@"size?3|" + file_way);                                        //发送指令来回收大小
                        int size = Client.Receive(@"size?3|" + file_way);                                   //接收大小
                        Client.SendCommand(@"3|" + file_way, size);                                     //发送指令来回收数据
                        byte[] re_str1 = Client.Receive(size, size);                                   //接收数据
                        string re_str = System.Text.Encoding.UTF8.GetString(re_str1, 0, size);
                        if (re_str == "|||***|||") { re_str = ""; }
                        Debug.WriteLine("第九" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                        Debug.WriteLine("txt" + re_str, DateTime.Now.ToShortTimeString());
                        file_way = "";
                        Debug.WriteLine("1234" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                        goToto = 1;                                                 //判断下一个页面去向
                        this.NavigationService.Navigate(new Uri("/File_Preview.xaml?top=" + name + "&doc=" + re_str, UriKind.Relative));
                    }
                    else
                    {
                        foreach (var lbi in List_Documents.SelectedItems)
                        {
                            if (lbi != null)
                            {
                                pitch_on_name = ((lbi as Documents_Item).Name).ToString();
                                pitch_on_image = ((lbi as Documents_Item).ImageUrl).ToString();
                            }
                        }
                        string cmdway = nowWay;
                        Client.SendCommand(@"size?4|" + cmdway + pitch_on_name);                                        //发送指令来回收大小
                        int size = Client.Receive(@"size?4|" + cmdway + pitch_on_name);                                   //接收大小
                        if(size < 4096000){
                            Client.SendCommand(@"4|" + cmdway + pitch_on_name, size);                                     //发送指令来回收数据
                            byte[] re_str = Client.Receive(size, size);                                   //接收数据
                            string re_str1 = System.Text.Encoding.UTF8.GetString(re_str, 0, size);
                            if (re_str1 == "|||***|||")
                            {
                                Save_Write_File_Now(re_str, 0);
                            }
                            else
                            {
                                Save_Write_File_Now(re_str, 1);
                            }
                        }
                        
                        pitch_on_name = "";                         //选中的名字
                        pitch_on_image = "";                         //选中的图片
                    }
                }
            }
            else if (isSelected == 1)                                       //重命名选中状态
            {
                isSelected = 0;
                foreach (var lbi in List_Documents.SelectedItems)
                {
                    if (lbi != null)
                    {
                        pitch_on_name = ((lbi as Documents_Item).Name).ToString();
                        pitch_on_image = ((lbi as Documents_Item).ImageUrl).ToString();
                    }
                }
                var input_Box = new InputPrompt
                {
                    Title = "重命名",
                    Message = "请输入新文件名称",
                };
                input_Box.Completed += ReName;
                input_Box.Show();
            }
            else if (isSelected == 2)                                  //保存选中状态
            {
                isSelected = 0;
                foreach (var lbi in List_Documents.SelectedItems)
                {
                    if (lbi != null)
                    {
                        pitch_on_name = ((lbi as Documents_Item).Name).ToString();
                        pitch_on_image = ((lbi as Documents_Item).ImageUrl).ToString();
                    }
                }
                if (pitch_on_image != "/images/Hard_Disk.png" && pitch_on_image != "/images/Folder.png")
                {
                    string cmdway = nowWay;
                    //if (nowWay != @"C:\" && nowWay != @"D:\" && nowWay != @"E:\" && nowWay != @"F:\" && nowWay != @"G:\" && nowWay != @"H:\" && nowWay != @"I:\")
                    //{
                    //    cmdway += @"\";
                    //}
                    Client.SendCommand(@"size?4|" + cmdway + pitch_on_name);                                        //发送指令来回收大小
                    int size = Client.Receive(@"size?4|" + cmdway + pitch_on_name);                                   //接收大小
                    Client.SendCommand(@"4|" + cmdway + pitch_on_name, size);                                     //发送指令来回收数据
                    byte[] re_str = Client.Receive(size, size);                                   //接收数据
                    string re_str1 = System.Text.Encoding.UTF8.GetString(re_str, 0, size);
                    if (re_str1 == "|||***|||")
                    {
                        Save_Write_File(re_str, 0);
                    }
                    else
                    {
                        Save_Write_File(re_str, 1);
                    }
                    Debug.WriteLine("第十" + re_str + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());

                    var messagePrompt = new MessagePrompt
                    {
                        Title = "保存文件",
                        Body = new TextBlock { Text = "成功保存文件！" },
                        IsAppBarVisible = true,
                    };
                    messagePrompt.Show();
                }
                else
                {
                    var messagePrompt = new MessagePrompt
                    {
                        Title = "保存文件",
                        Body = new TextBlock { Text = "无法保存文件夹！" },
                        IsAppBarVisible = true,
                    };
                    messagePrompt.Show();
                }
                pitch_on_name = "";                         //选中的名字
                pitch_on_image = "";                         //选中的图片

                Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
                int size1 = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
                Client.SendCommand(@"1|" + nowWay, size1);                                     //发送指令来回收数据
                string re_str2 = Client.Receive(size1);                                   //接收数据

                Debug.WriteLine("第十一" + re_str2 + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
                this.top_Text.DataContext = new Now_Top(nowName);
                Bind_Data(re_str2);
            }
            else if (isSelected == 3)                                       //删除选中状态
            {
                isSelected = 0;
                foreach (var lbi in List_Documents.SelectedItems)
                {
                    if (lbi != null)
                    {
                        pitch_on_name = ((lbi as Documents_Item).Name).ToString();
                        pitch_on_image = ((lbi as Documents_Item).ImageUrl).ToString();
                    }
                }
                var messagePrompt = new MessagePrompt
                {
                    Title = "删除文件",
                    Body = new TextBlock { Text = "是否确定删除？" },
                    IsAppBarVisible = true,
                    IsCancelVisible = true
                };
                messagePrompt.Completed += select;
                messagePrompt.Show();
            }
        }

        public async void Save_Write_File_Now(byte[] content, int key)
        {
            Debug.WriteLine("第十二ryryryry" + content + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
            StorageFolder storageFolder = KnownFolders.PicturesLibrary;                                             //写入文件
            StorageFile file = await storageFolder.CreateFileAsync(pitch_on_name, CreationCollisionOption.ReplaceExisting);
            //将指定的文本内容写入到指定的文件
            if (key == 1)
            {
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    await stream.WriteAsync(content, 0, content.Length);
                }
            }
            goToto = 1;
            // 启动与 .log 类型文件关联的默认应用程序，来打开指定的文件
            Windows.System.Launcher.LaunchFileAsync(file);

        }
        public async void Save_Write_File(byte[] content, int key)
        {
            Debug.WriteLine("第十二ryryryry" + content + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
            StorageFolder storageFolder = KnownFolders.PicturesLibrary;                                             //写入文件
            StorageFile file = await storageFolder.CreateFileAsync(pitch_on_name, CreationCollisionOption.ReplaceExisting);
            //将指定的文本内容写入到指定的文件
            if (key == 1)
            {
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    await stream.WriteAsync(content, 0, content.Length);
                }
            }

        }
        void select(object sender, PopUpEventArgs<string, PopUpResult> e)                     //删除文件
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                string cmdway = nowWay;
                if (nowWay != @"C:\" && nowWay != @"D:\" && nowWay != @"E:\" && nowWay != @"F:\" && nowWay != @"G:\" && nowWay != @"H:\" && nowWay != @"I:\")
                {
                    cmdway += @"\";
                }
                if (pitch_on_image == "/images/Hard_Disk.png" || pitch_on_image == "/images/Folder.png")
                {
                    Client.SendCommand(@"8|" + cmdway + pitch_on_name);
                }
                else
                {
                    Client.SendCommand(@"9|" + cmdway + pitch_on_name);
                }
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
            pitch_on_name = "";                         //选中的名字
            pitch_on_image = "";                         //选中的图片

            Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
            int size = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
            Client.SendCommand(@"1|" + nowWay, size);                                     //发送指令来回收数据
            string re_str1 = Client.Receive(size);                                   //接收数据
            Debug.WriteLine("第十二" + re_str1 + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
            this.top_Text.DataContext = new Now_Top(nowName);
            Bind_Data(re_str1);
        }

        void ReName(object sender, PopUpEventArgs<string, PopUpResult> e)                         //重命名文件
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                string cmdway = nowWay;
                if (nowWay != @"C:\" && nowWay != @"D:\" && nowWay != @"E:\" && nowWay != @"F:\" && nowWay != @"G:\" && nowWay != @"H:\" && nowWay != @"I:\")
                {
                    cmdway += @"\";
                }
                if (pitch_on_image == "/images/Hard_Disk.png" || pitch_on_image == "/images/Folder.png")
                {
                    Client.SendCommand(@"5|" + cmdway + pitch_on_name + "|" + e.Result);                     //不需要返回，不加size
                }
                else
                {
                    Client.SendCommand(@"7|" + cmdway + pitch_on_name + "|" + e.Result);
                }
            }
            else if (e.PopUpResult == PopUpResult.Cancelled)
            {
                //MessageBox.Show("CANCELLED! " + e.Result);
            }
            pitch_on_name = "";                         //选中的名字
            pitch_on_image = "";                         //选中的图片
            Client.SendCommand(@"size?1|" + nowWay);                                        //发送指令来回收大小
            int size = Client.Receive(@"size?1|" + nowWay);                                   //接收大小
            Client.SendCommand(@"1|" + nowWay, size);                                     //发送指令来回收数据
            string re_str1 = Client.Receive(size);                                   //接收数据

            Debug.WriteLine("第十三" + re_str1 + "+" + nowName + "+" + nowWay + "+" + lastWay, DateTime.Now.ToShortTimeString());
            this.top_Text.DataContext = new Now_Top(nowName);
            Bind_Data(re_str1);
        }
    }
}