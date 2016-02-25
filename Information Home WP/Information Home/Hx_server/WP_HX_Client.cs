using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;


namespace Information_Home
{
    public class WP_HX_Client
    {
        public static Socket Socket;
        public static ManualResetEvent MyEvent = null;
        const int MAX_SIZE = 2048;
        Boolean connected = false;
        public WP_HX_Client()
        {
            Socket = null;
        }
        public void Leave_Out()
        {
            if (WP_HX_Client.Socket != null)
            {
                WP_HX_Client.Socket.Close();
            }
            WP_HX_Client.Socket = null;
        }
        public Boolean Client_HX(string IP)  //连接函数     无参数
        {
            if (Socket == null)
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            if (MyEvent == null)
            {
                MyEvent = new ManualResetEvent(false);
            }

            if (Socket != null)
            {
                SocketAsyncEventArgs connArg = new SocketAsyncEventArgs();
                connArg.RemoteEndPoint = new DnsEndPoint(IP, 6666);


                connArg.Completed += (sendObj, arg) =>
                {
                    if (arg.SocketError == SocketError.Success)
                    {
                        // MessageBox.Show("连接成功");
                        connected = true;
                    }
                    else
                    {
                        //MessageBox.Show("连接失败，错误：" + arg.SocketError.ToString());

                    }

                    MyEvent.Set();
                };

                MyEvent.Reset();
                Socket.ConnectAsync(connArg);
                MyEvent.WaitOne(2000);

            }
            return connected;

        }
        public void Send_list(string txt)     //发送函数    txt为发送你容
        {
            if (Socket != null && Socket.Connected)
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
                Socket.SendAsync(sendArg);
                MyEvent.WaitOne(6000);
            }
        }
        public string Re_List()   //接收函数       response为接收内容 
        {

            string response = "Operation Timeout";
            if (connected)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                if (socketEventArg != null)
                {
                    socketEventArg.RemoteEndPoint = Socket.RemoteEndPoint;
                    socketEventArg.SetBuffer(new Byte[MAX_SIZE], 0, MAX_SIZE);
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
                    Socket.ReceiveAsync(socketEventArg);
                    MyEvent.WaitOne(-1);
                    }
            }
            else
            {
                response = null;
                
            }
            return response;

        }
    }
}
