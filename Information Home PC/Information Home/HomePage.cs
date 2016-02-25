
/**********************************************************************
 *                              Imagine Cup 2015 
 * 
 *                                              I.Pigeon
 *     Information  Home    
 *         
 *     Finish Time                    The End Line : 31/3/2015
 *         
 **********************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Home_PC
{
    public partial class HomePage : Form
    {

        private Server_PC server = new Server_PC();
        public static ShareForm shareform = new ShareForm();
        public MyAudio myaudio = new MyAudio();
        HX_Client hx = new HX_Client();
        public ListViewItem lvi = null;
        private Thread thread1;
        public Thread thread;
        public HomePage(string IP)
        {
            if (!Directory.Exists(@"C:\Users\Public\IHomeFiles"))
            {
                Directory.CreateDirectory(@"C:\Users\Public\IHomeFiles");
            }
            InitializeComponent();
            server.Set_Server();
            //MessageBox.Show(IP);
            if (hx.Connect_HX(IP))
            {

                hx.Send_List("CONN|longshao1");
                thread1 = new Thread(new ThreadStart(Show_List));
                thread1.Start();
                this.ShowMess.Font = new Font("宋体", 8, FontStyle.Bold);
                this.ShowMess.ForeColor = Color.White;
                this.ShowMess.Text = "请将手机连入大厅地址：" +IP;
            }
            else
            {
                MessageBox.Show("大厅未开启！But，本身服务器已建立，仍然可以被手动连接。");
                this.ShowMess.Font = new Font("宋体", 10, FontStyle.Bold);
                this.ShowMess.ForeColor = Color.White;
                this.ShowMess.Text = "请将手机连入IP：" + GetIP();
            }

        }
        public HomePage()
        {
            if (!Directory.Exists(@"C:\Users\Public\IHomeFiles"))
            {
                Directory.CreateDirectory(@"C:\Users\Public\IHomeFiles");
            }
            InitializeComponent();
            server.Set_Server();
            MessageBox.Show("大厅未连接！But，本身服务器已建立，仍然可以被手动连接。");
            this.ShowMess.Font = new Font("宋体", 10, FontStyle.Bold); 
            this.ShowMess.ForeColor = Color.White;
            this.ShowMess.Text = "请将手机连入IP：" + GetIP();
        }

        public void setItems(object str)  //跨线程调用控件
        {
            if (true)
            {
                Action<string> actionDelegate = (x) =>
                {
                    this.listView1.Items.Add(lvi);
                    this.listView1.EndUpdate();
                };
                this.listView1.Invoke(actionDelegate, str);
            }
        }


        public void Show_List()
        {
            List<information> List_User = new List<information>();
            information l_LAVE = new information();
            //MessageBox.Show("lol"+hx.Receive_List());
            while (true)
            {
                try
                {
                    string[] tokens = hx.Receive_List().Split(new Char[] { '|' });
                    //MessageBox.Show("lol"+hx.Receive_List());
                    List_User = (List<information>)FromJson(tokens[1]);

                    // if (List_User != null)
                    //           {
                    if (tokens[0] == "Client")
                    {
                        foreach (information p in List_User)
                        {
                            New_User_information(p.name, p.ip);

                            thread = new Thread(new ParameterizedThreadStart(setItems));
                            thread.Start();

                        }
                        // List_User.Clear();
                    }
                    else if (tokens[0] == "Quit")
                    {
                        //
                       // MessageBox.Show(tokens[1]);
                        l_LAVE = (information)FromJson_leave(tokens[1]);

                        //MessageBox.Show("用户退出" + l_LAVE.name + "   " + l_LAVE.ip);

                        //.  List_User.Clear();
                        foreach (ListViewItem item in listView1.Items)           //索引删除
                        {
                            if (item.SubItems[1].Text == l_LAVE.name)
                            {
                                listView1.Items.Remove(item);
                            }
                        }
                    }
                }catch(Exception a){
                 
                }
            }
        }
        public List<information> FromJson(string json)
        {
            List<information> jsonObject = null;
            try
            {
                jsonObject = (List<information>)JsonConvert.DeserializeObject(json, typeof(List<information>));

            }
            catch (Exception e)
            {
               
            }

            return jsonObject;
        }
        public information FromJson_leave(string json)
        {
            information jsonO = null;
            try
            {
                jsonO = (information)JsonConvert.DeserializeObject(json, typeof(information));


            }
            catch (Exception e)
            {
                
            }
            return jsonO;

        }
        private string GetIP()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();

        }


        // 关闭窗体
        private void close_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("您要退出 I - Home ? ", "温馨提示",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question) == DialogResult.OK)
            {

                string ip = GetIP();

                hx.Send_List("Signout|" + ip);
                hx.Leave_Out_HX();
                MyDos.ExecDosInstructions(@"taskkill /f /im Information Home PC.exe");
                server.Leave_Out();
                
                ExitNotifyIcon();
                AnimateWindow(this.Handle, 2000, AW_BLEND | AW_HIDE | AW_VER_NEGATIVE);
                //MyDos.ExecDosInstructions(@"taskkill /f /im Information Home PC.exe");
                Application.Exit();
            }
        }

        // 窗体最小化
        private void min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false; //隐藏任务栏区图标
        }

        // 还原窗体
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.MainForm_Resize();  // 双缓存加载窗体
            WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;  // 任务栏区显示图标 
        }
        public void New_User_information(string name, string u_IP)
        {
            lvi = new ListViewItem();      //列表程序
            lvi.Text = u_IP;
            lvi.SubItems.Add(name);

        }

        // 内容分享
        private void share_Click(object sender, EventArgs e)
        {
            ShareShow();
        }

        public static void ShareShow()
        {
            if (MessageBox.Show("您要进行内容分享么? ", "温馨提示",
      MessageBoxButtons.OKCancel,
      MessageBoxIcon.Question) == DialogResult.OK)
            {
                shareform.Show();
            }
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
    }
}