using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using LitJson;


namespace HX_Server
{
    [Serializable]
    public partial class HX_server : Form
    {
        static Socket mySocket;
        static Socket socket;
        static Socket _socket;
        public  ListViewItem lvi = null;
        public ListViewItem lvi2 = null;
        static int ii = 0;

        //private Hashtable _transmit_tb = new Hashtable();
        private List<Client_information> User = new List<Client_information>();
        private List<information>User_information = new List<information>();
        private List<information> newUser_list = new List<information>();
        BinaryFormatter binFormat = new BinaryFormatter();
        Thread thread;
        Thread th;
        
        //private Client_information[] User = new Client_information[50];
        public HX_server()
        {
            mySocket = null;
            socket = null;
            _socket = null;
            
            InitializeComponent();
            textBox1.AppendText(GetIP());

        }
        public void Set_Server()  //建立服务器
        {
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint local = new IPEndPoint(IPAddress.Any, 6666);
            mySocket.Bind(local);
            mySocket.Listen(100);



            mySocket.BeginAccept(new AsyncCallback(this.AcceptSocketCallback), mySocket);
            
            MessageBox.Show("大厅成功开启");
        }
        private void AcceptSocketCallback(IAsyncResult ia)  //异步通信回调函数 , 接收socket回调
        {
            socket = ia.AsyncState as Socket;
            Socket accptSocket = socket.EndAccept(ia);
            try
            {

                IPEndPoint remote = (IPEndPoint)accptSocket.RemoteEndPoint;   //强制类型转换为 地址族   用来显示远程IP
                //MessageBox.Show(remote.Address.ToString());
                StateObject so = new StateObject();
                so.theSocket = accptSocket;

                accptSocket.BeginReceive(so.Buffer, 0, so.Buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), so);
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            socket.BeginAccept(new AsyncCallback(this.AcceptSocketCallback), socket);
        }
        public void ReceiveCallback(IAsyncResult ia)     //接收回调   msg为接收数据
        {
            StateObject _so = ia.AsyncState as StateObject;
            _socket = _so.theSocket;
            try
            {
                //接收到的字节数  
                int n = _socket.EndReceive(ia);
                string msg = Encoding.UTF8.GetString(_so.Buffer, 0, n);
                string[] tokens = msg.Split(new Char[]{'|'});
               
                IPEndPoint m = (IPEndPoint)_socket.RemoteEndPoint;


                
                if (tokens[0] == "CONN")
                {
                    Client_information New_User = new Client_information(tokens[1], m.Address.ToString(), _socket);
                    information New_User_INFOR = new information(tokens[1], m.Address.ToString());
                    New_User_information(tokens[1], m.Address.ToString());

                    thread = new Thread(new ParameterizedThreadStart(setItems));
                    thread.Start();
                    
                    MemoryStream User_Stream = new MemoryStream();
                    newUser_list.Clear();
                    newUser_list.Add(New_User_INFOR);

                    foreach (Client_information p in User)
                    {
                       // Client_information cl = (Client_information)User[i];
                        string js = "Client|"+JsonConvert.SerializeObject(newUser_list);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(js);
                        SendCommand(p, buffer);
                        
                        
                    }
                    /*for (int i = 0;i<User.Count; i++)
                    {                     
                        Client_information cl = (Client_information)User[i];
                        string js = JsonConvert.SerializeObject(newUser_list);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(js);
                        SendCommand(cl, buffer);
                        
                    }*/
                    string json = "Client|"+JsonConvert.SerializeObject(User_information);
                    
                    byte[] bf = System.Text.Encoding.UTF8.GetBytes(json);
                    SendCommand(New_User,bf);
                   // MessageBox.Show("ok");
                    
                    User.Add(New_User);
                    User_information.Add(New_User_INFOR);
                   // MessageBox.Show(User_information[0].ip+"OK");

                    //MessageBox.Show(User[0].getName());
          
                }
                if (tokens[0] == "Signout")
                {
                    string js = null;
                    byte[] buffer = null;
                    for(int i =0;i<User_information.Count;i++)
                    {

                        if(User_information[i].ip.Equals(tokens[1]))
                        {
                             js = "Quit|"+JsonConvert.SerializeObject(User_information[i]);
                             buffer = System.Text.Encoding.UTF8.GetBytes(js);
                             ii =i;
                             New_Leave(User_information[i].name, User_information[i].ip);
                             th = new Thread(new ParameterizedThreadStart(longshao));
                             th.Start();
                             leave_OUT(User[i]);
                             User_information.RemoveAt(i);
                             User.RemoveAt(i);
                        }
                    }
                    for(int i =0;i<User.Count;i++)
                    {
                         buffer = System.Text.Encoding.UTF8.GetBytes(js);
                         SendCommand(User[i], buffer);
                    }

                }
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
                
            //继续接收
            _so = new StateObject();
            _so.theSocket = _socket;
            try
            {
                _socket.BeginReceive(_so.Buffer, 0, _so.Buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), _so);
            }
            catch(Exception e)
            {
            }
           
        }

        public void SendCommand(Client_information user,byte[] txt)     //发送函数    txt为发送内容
        {
        
           
            if (user.Sock != null && user.Sock.Connected)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                sendArg.SetBuffer(txt, 0, txt.Length);
                sendArg.Completed += (objSender, mArg) =>
                {
                    if (mArg.SocketError == SocketError.Success)
                    {
                        // this.BeginInvoke(new Action<string>(this.Show_Message2), "发送成功");
                    }
                    else
                    {
                        //  this.BeginInvoke(new Action<string>(this.Show_Message2), "发送失败，错误：" + mArg.SocketError.ToString());
                    }
                };
                user.Sock.SendAsync(sendArg);   
            }
        }
        public void setItems(object str)  //跨线程调用控件
        {
            if(true)
            {                
                Action<string> actionDelegate = (x) =>
                {                    
                    this.listView1.Items.Add(lvi);
                    this.listView1.EndUpdate();
                };
                this.listView1.Invoke(actionDelegate, str);
            }
        }
        public void New_User_information(string name, string u_IP)
        {
            lvi = new ListViewItem();      //列表程序
            lvi.Text = u_IP;
            lvi.SubItems.Add(name);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Set_Server();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.listView1.Items.RemoveAt(0);
        }
        public void New_Leave(string name, string ip)
        {
            lvi2 = new ListViewItem();
            lvi2.Text = ip;
            lvi2.SubItems.Add(name);
        }

        public void longshao(object str)  //跨线程调用控件
        {
            if (true)
            {
                Action<string> actionDelegate = (x) =>
                {
                    this.listView1.Items.RemoveAt(ii);
                    this.listView1.EndUpdate();
                };
                this.listView1.Invoke(actionDelegate, str);
            }
        }
        public void leave_OUT(Client_information p)
        {
            if(p.socket!=null)
            {
                p.socket.Close();
            }
            p.socket = null;
        }
        private string GetIP()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }


}
