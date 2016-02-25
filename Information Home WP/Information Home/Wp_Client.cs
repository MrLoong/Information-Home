using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.Windows;
using System.Diagnostics;

namespace Information_Home
{
    public class WP_Client
    {
        public static Socket mySocket;
        public static ManualResetEvent MyEvent = null;
        const int MAX_BUFFER_SIZE = 10240;

        public WP_Client()
        {
            mySocket = null;
            //ManualResetEvent MyEvent = null;
        }

        public void Leave_Out()
        {
            if (WP_Client.mySocket != null)
            {
                WP_Client.mySocket.Close();
            }
            WP_Client.mySocket = null;
        }

        public Boolean Connect(string IP)  //连接函数     无参数
        {
            Boolean key = false;
            if (mySocket == null)
            {
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            if (MyEvent == null)
            {
                MyEvent = new ManualResetEvent(false);
            }

            if (mySocket != null)
            {
                SocketAsyncEventArgs connArg = new SocketAsyncEventArgs();
                connArg.RemoteEndPoint = new DnsEndPoint(IP, 6667);


                connArg.Completed += (sendObj, arg) =>
                {
                    if (arg.SocketError == SocketError.Success)
                    {
                        key = true;
                    }
                    else
                    {
                    }

                    MyEvent.Set();
                };

                MyEvent.Reset();
                mySocket.ConnectAsync(connArg);
                MyEvent.WaitOne(6000);
            }
            return key;

        }
        public void SendCommand(string txt)     //发送函数    txt为发送你容
        {
            if (mySocket != null && mySocket.Connected)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
                sendArg.SetBuffer(buffer, 0, buffer.Length);
                sendArg.Completed += (objSender, mArg) =>
                {

                    if (mArg.SocketError == SocketError.Success)
                    {
                        Debug.WriteLine(txt, DateTime.Now.ToLongTimeString());
                    }
                    else
                    {
                    }
                    MyEvent.Set();
                };
                MyEvent.Reset();
                mySocket.SendAsync(sendArg);
                MyEvent.WaitOne(6000);
            }
        }

        public void SendCommand(string txt, int size)     //发送函数    txt为发送你容
        {
            if (mySocket != null && mySocket.Connected)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt.ToString());
                sendArg.SetBuffer(buffer, 0, buffer.Length);

                sendArg.Completed += (objSender, mArg) =>
                {

                    if (mArg.SocketError == SocketError.Success)
                    {
                    }
                    else
                    {
                    }

                    MyEvent.Set();
                };
                MyEvent.Reset();
                mySocket.SendAsync(sendArg);
                MyEvent.WaitOne(6000);
            }
        }
        public void SendCommand(byte[] txt)     //发送函数    txt为发送你容
        {
            if (mySocket != null && mySocket.Connected)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                byte[] buffer = txt;
                sendArg.SetBuffer(buffer, 0, buffer.Length);
                sendArg.Completed += (objSender, mArg) =>
                {
                    if (mArg.SocketError == SocketError.Success)
                    {
                    }
                    else
                    {
                    }
                    MyEvent.Set();
                };
                MyEvent.Reset();
                mySocket.SendAsync(sendArg);
                MyEvent.WaitOne(6000);
            }
            else
            {
                Debug.WriteLine("连不上",DateTime.Now.ToShortTimeString());
            }
        }
        public int Receive(string txt)   //接收函数       response为接收内容的大小
        {
            int response = 0;
            if (mySocket != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = mySocket.RemoteEndPoint;
                socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        Debug.WriteLine("size" + System.Text.Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred), DateTime.Now.ToShortTimeString());
                        response = int.Parse(System.Text.Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred));              //字符串转int
                    }
                    else
                    {
                        Debug.WriteLine(e.SocketError.ToString(), DateTime.Now.ToShortTimeString());
                    }
                    MyEvent.Set();
                });
                MyEvent.Reset();
                mySocket.ReceiveAsync(socketEventArg);
                MyEvent.WaitOne(-1);
            }
            else
            {
                response = 1;
            }
            Debug.WriteLine("size" + response.ToString(), DateTime.Now.ToShortTimeString());
            return response;

        }
        public string Receive(int size)   //接收函数       response为接收内容 
        {
            string response = "Operation Timeout";
            if (size == 0) { size = 10; }
            if (mySocket != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = mySocket.RemoteEndPoint;
                socketEventArg.SetBuffer(new Byte[size], 0, size);
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
                    // 帮我找到服务端
                    MyEvent.Set();
                });
                MyEvent.Reset();
                mySocket.ReceiveAsync(socketEventArg);
                MyEvent.WaitOne(-1);
            }
            else
            {
                response = "Socket is not initialized";
            }
            Debug.WriteLine("string" + response, DateTime.Now.ToShortTimeString());
            return response;

        }
        public byte[] Receive(int size, int b)   //接收函数       response为接收内容 
        {
            if (size == 0) { size = 10; }
            byte[] response = new byte[size];
            if (mySocket != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = mySocket.RemoteEndPoint;
                socketEventArg.SetBuffer(new Byte[size], 0, size);
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        response = e.Buffer;
                    }
                    else
                    {
                        //response = e.SocketError.ToString();
                    }
                    MyEvent.Set();
                });
                MyEvent.Reset();
                mySocket.ReceiveAsync(socketEventArg);
                MyEvent.WaitOne(-1);
            }
            else
            {
            }
            Debug.WriteLine("byte[]" + response.Length.ToString(), DateTime.Now.ToShortTimeString());
            return response;
        }
    }
}

