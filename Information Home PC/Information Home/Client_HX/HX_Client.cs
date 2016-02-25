using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Information_Home_PC
{
    public class HX_Client
    {
        public static Socket my_Socket = null;
        public static ManualResetEvent MyEvent = null;
        const int MAX_BUFFER_SIZE = 2048;
        Boolean connected = false;
        public HX_Client()
        {
            my_Socket = null;
        }
        public Boolean Connect_HX(string IP)  //连接函数     无参数
        {
            if (my_Socket == null)
            {
                my_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            if (MyEvent == null)
            {
                MyEvent = new ManualResetEvent(false);
            }

            if (my_Socket != null)
            {
                SocketAsyncEventArgs connArg = new SocketAsyncEventArgs();
                connArg.RemoteEndPoint = new DnsEndPoint(IP, 6666);
                connArg.Completed += (sendObj, arg) =>
                {
                    if (arg.SocketError == SocketError.Success)
                    {
                         //MessageBox.Show("连接成功");
                        connected = true;
                    }
                    else
                    {
                        //MessageBox.Show("连接失败，错误：" + arg.SocketError.ToString());
                    }

                    MyEvent.Set();
                };

                MyEvent.Reset();
                my_Socket.ConnectAsync(connArg);
                MyEvent.WaitOne(2000);
            }
            return connected;

        }
        public void Send_List(string txt)     //发送函数    txt为发送你容
        {
            if (my_Socket != null && my_Socket.Connected)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
                sendArg.SetBuffer(buffer, 0, buffer.Length);

                sendArg.Completed += (objSender, mArg) =>
                {

                    if (mArg.SocketError == SocketError.Success)
                    {
                        //MessageBox.Show("发送成功");
                    }
                    else
                    {
                        //MessageBox.Show("发送失败，错误：" + mArg.SocketError.ToString());

                    }
                    MyEvent.Set();
                };
                MyEvent.Reset();
                my_Socket.SendAsync(sendArg);
                MyEvent.WaitOne(6000);
            }
        }
        public string Receive_List()   //接收函数       response为接收内容 
        {
            string response = "Operation Timeout";
            if (connected)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                if (socketEventArg!=null)
                {
                    socketEventArg.RemoteEndPoint = my_Socket.RemoteEndPoint;
                    socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);
                    socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                    {
                        if (e.SocketError == SocketError.Success)
                        {
                            response = System.Text.Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                            response = response.Trim('\0');
                        }
                        else
                        {
                            response = e.SocketError.ToString();
                        }

                        MyEvent.Set();
                    });

                    MyEvent.Reset();
                    my_Socket.ReceiveAsync(socketEventArg);
                    MyEvent.WaitOne(-1);
                }
            }
            else
            {
                response = null;
                
            }
            return response;
        }
        public void Leave_Out_HX()
        {
            if (HX_Client.my_Socket != null)
            {
                HX_Client.my_Socket.Close();
            }
            HX_Client.my_Socket = null;
        }
    }
}
