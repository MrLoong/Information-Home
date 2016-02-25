using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; 
using System.Net.Sockets;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Media;
using Server_pc;

namespace Information_Home_PC
{
    public class Server_PC
    {
        private MyInstruction cmd = new MyInstruction();
        private string findContent = "";
        public string ReceiveContent = "";
        public static Socket mySocket;
        public static Socket socket;
        public static Socket _socket;
        int key = 0;
        int key2 = 0;
        private string Pattern = "File sharing";
        ShareForm shareform;
        VoiceCall sharevoice;
        SoundPlayer sound = new SoundPlayer();
        public MyAudio my_Audio = new MyAudio();
        Thread threadVoice;
        private string name = "";
        Thread Voice_t;
        Thread threadClose;
        int closei = 0;

        [DllImport("user32.dll")]                              //鼠标加载
        static extern bool GetCursorInfo(out CURSORINFO pci);
        private const Int32 CURSOR_SHOWING = 0x00000001;
        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public Int32 x;
            public Int32 y;
        } 

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }
        public Server_PC()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            mySocket = null;
            socket = null;
            _socket = null;
        }
        public void Set_Server()  //建立服务器
        {
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint local = new IPEndPoint(IPAddress.Any, 6688);
            mySocket.Bind(local);
            mySocket.Listen(100);
            Console.WriteLine("set");
            mySocket.BeginAccept(new AsyncCallback(this.AcceptSocketCallback), mySocket);
        }
        // 当客户端连接时执行该函数
        private void AcceptSocketCallback(IAsyncResult ia)  //异步通信回调函数 , 接收socket回调
        {
            socket = ia.AsyncState as Socket;
            
            try
            {
                Socket accptSocket = socket.EndAccept(ia);
                IPEndPoint remote = (IPEndPoint)accptSocket.RemoteEndPoint;   //强制类型转换为 地址族   用来显示远程IP
                StateObject so = new StateObject();
                so.theSocket = accptSocket;
                accptSocket.BeginReceive(so.Buffer, 0, so.Buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), so);
                socket.BeginAccept(new AsyncCallback(this.AcceptSocketCallback), socket);
            }
            catch(Exception ex)
            {
                
            }

            
        }
        // 当客户端发送消息时执行该函数，接收Socket中的数据
        public void ReceiveCallback(IAsyncResult ia)     //接收回调   ReceiveContent为接收数据
        {
            key = 0;
            StateObject _so = ia.AsyncState as StateObject;
            _socket = _so.theSocket;
            
            if (Pattern == "File sharing")
            {
                try
                {
                    int n = _socket.EndReceive(ia);     // 接收到的字节数
                    ReceiveContent = Encoding.UTF8.GetString(_so.Buffer, 0, n);
                    Console.WriteLine(ReceiveContent, DateTime.Now.ToLongTimeString());
                    string[] type = ReceiveContent.Split('?');
                    if (type[0].Equals("size"))                   //获取数据大小
                    {
                        Console.WriteLine(type[0] + "+" + type[1], DateTime.Now.ToLongTimeString());
                        key = 1;
                        string findContent_Size = cmd.Exec(type[1]);
                        if (findContent_Size.Equals("HQNR"))                                 // 如果数据为Byte[]
                        {
                            int i = cmd.Get_Byte().Length;
                            SendCommand(i);
                        }
                        else if (findContent_Size.Equals("PMTS"))
                        {
                            SendCommand(cmd.Get_Byte());
                        }
                        else
                        {
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(findContent_Size);                 //如果是字符串
                            SendCommand(buffer.Length);                                               // 发送大小
                        }
                        Console.WriteLine("size+string" + findContent_Size, DateTime.Now.ToLongTimeString());
                    }
                    else if (type[0].Equals("play"))                           //跳转到屏幕传输
                    {
                        PlayBells.StartPlayMedia("DoorBell.wav");
                        key = 1;
                        if (MessageBox.Show("请求分享风景？", "温馨提示",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            PlayBells.StopPlayMedia();
                            SendCommand("OK");
                            Pattern = "Scenic sharing";
                            Thread thread_show = new Thread(ShowShow);
                            thread_show.Start();
                            threadClose = new Thread(ShowClose);
                            threadClose.Start();
                        }
                        else
                        {
                            PlayBells.StopPlayMedia();
                            SendCommand("NO");
                        }
                    }
                    else if (type[0].Equals("doorbells"))
                    {
                        PlayBells.StartPlayMedia("DoorBell.wav");
                        key = 1;
                        if (MessageBox.Show("是否接受视频通话？", "客人来访", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            PlayBells.StopPlayMedia();
                            SendCommand("OK");
                            Pattern = "Scenic sharing";
                            Thread thread_show = new Thread(ShowShow);//创建新线程  
                            thread_show.Start();
                                
                        }
                        else
                        {
                            PlayBells.StopPlayMedia();
                            SendCommand("NO");
                        }
                    }
                    else if (type[0].Equals("voice"))                           //跳转到屏幕传输
                    {
                        PlayBells.StartPlayMedia("DoorBell.wav");
                        key = 1;
                        if (MessageBox.Show("请求语音通话？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            PlayBells.StopPlayMedia();
                            SendCommand("OK");
                            Pattern = "Voice Calls";
                            threadVoice = new Thread(ShowVoice);//创建新线程 
                            threadVoice.Start();
                        }
                        else
                        {
                            PlayBells.StopPlayMedia();
                            SendCommand("NO");
                        }
                    }
                    else if (type[0].Equals("data"))
                    {
                        key = 1;
                        Pattern = "DataSharing";
                    }
                    else if (type[0].Equals(""))
                    {
                        return;
                    }
                    else                             //获取数据或执行功能
                    {
                        Console.WriteLine(type[0], DateTime.Now.ToLongTimeString());
                        findContent = cmd.Exec(type[0]);
                    }
                }
                catch (Exception ex)
                {
                    
                }
                if (!findContent.Equals("wcz") && !findContent.Equals("HQNR") && !findContent.Equals("PMTS") && key == 0)
                {
                    Console.WriteLine("string" + findContent, DateTime.Now.ToLongTimeString());
                    SendCommand(findContent);                                               // 发送字符串
                    findContent = "";
                }
                else if (findContent.Equals("HQNR") && key == 0)
                {
                    SendCommand(cmd.Get_Byte());                                           // 发送byte[]
                    findContent = "";
                }
                ReceiveContent = "";       

            }
            else if (Pattern == "Scenic sharing")
            {
                try
                {
                    int n = _socket.EndReceive(ia);//接收到的字节数 
                    Console.WriteLine(n.ToString(), DateTime.Now.ToLongTimeString());
                    ReceiveContent = Encoding.UTF8.GetString(_so.Buffer, 0, n);
                    if (_so.Buffer == null || ReceiveContent == "|||***|||")
                    {
                        threadClose.Abort();
                        Pattern = "File sharing";
                        shareform.Close();
                    }
                    else
                    {
                        closei = 0;
                        Console.WriteLine("shenjing", DateTime.Now.ToLongTimeString());
                        byte[] buffer = new byte[n];
                        Image im = ChangeImageWithByte.BytToImg(_so.Buffer, n);      // 位图和字符流之间的转换
                        shareform.Back_Image(im);
                    }
                }
                catch (Exception ex)
                {

                }
                ReceiveContent = "";      

            }
            else if (Pattern == "Voice Calls")
            {
                MemoryStream stream = new MemoryStream();
                WavEncoder wav = new WavEncoder();
                try
                {
                    int n = _socket.EndReceive(ia);//接收到的字节数  
                    stream.Write(_so.Buffer, 0, n);
                    ReceiveContent = Encoding.UTF8.GetString(_so.Buffer, 0, n);
                    if (_so.Buffer == null || ReceiveContent == "|||***|||")
                    {
                        Pattern = "File sharing";
                        Voice_t.Abort();
                        sharevoice.Close();
                    }
                    else if (ReceiveContent == "start")
                    {
                        sharevoice.threadvoi = new Thread(MyAudio.Send_Voice);
                        sharevoice.threadvoi.Start();
                        Voice_t.Abort();
                        sharevoice.Change();
                    }
                    else
                    {
                        sound.Stream = wav.Encode(stream);
                        //System.Threading.Thread.Sleep(50);
                        Voice_t = new Thread(new ThreadStart(sound.Play));
                        Voice_t.Start();
                    }
                    
                }
                catch
                {
                 
                }
                ReceiveContent = "";      
            }
            else if (Pattern == "DataSharing")
            {
                //name = "哈哈.mp4";
                try
                {
                    int n = _socket.EndReceive(ia);//接收到的字节数 
                    Console.WriteLine(n.ToString(), DateTime.Now.ToLongTimeString());
                    ReceiveContent = Encoding.UTF8.GetString(_so.Buffer, 0, n);
                    if (ReceiveContent != "|||***|||")
                    {
                        string[] data = ReceiveContent.Split('|');
                        if (data[0] == "FileName")
                        {
                            name = data[1];
                        }
                        else
                        {
                            writeFile(_so.Buffer, @"C:\Users\Public\IHomeFiles\" + name, n);
                            MyDos.ExecDosInstructions(@"C:\Users\Public\IHomeFiles\" + name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("shareform.Close();", DateTime.Now.ToLongTimeString());
                        Pattern = "File sharing";
                        //shareform.Close();
                    }
                }
                catch (Exception ex)
                {
                    
                  
                }
                ReceiveContent = "";      
            }
            // 继续接收
            _so = new StateObject();
            _so.theSocket = _socket; 
            try
            {
                _socket.BeginReceive(_so.Buffer, 0, _so.Buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), _so);
            }
            catch (Exception ex)
            {
           
            }
        }
         private bool writeFile(byte[] pReadByte, string fileName,int a)
        {
            FileStream pFileStream = null;
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, a);
            }
            catch
            {
               
                return false;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
            return true;
        }
        public void ShowShow()
        {
            shareform = new ShareForm();
            shareform.ShowDialog();
        }
        public void ShowVoice()
        {
            sharevoice = new VoiceCall();
            sharevoice.Location = new Point(SystemInformation.PrimaryMonitorSize.Width,
                                        SystemInformation.PrimaryMonitorSize.Height);
            sharevoice.ShowDialog();
        }
        public void ShowClose()
        {
            for (; closei < 2; closei++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("滴滴滴滴滴" + closei, DateTime.Now.ToLongTimeString());
            }
            if (closei == 2)
            {
                Pattern = "File sharing";
                shareform.Close();
            }
        }
        public static void SendCommand(string txt)     // 发送函数    发送字符串
        {

            if (_socket != null && _socket.Connected)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
                sendArg.SetBuffer(buffer, 0, buffer.Length);
                sendArg.Completed += (objSender, mArg) =>
                {
                    if (mArg.SocketError == SocketError.Success)
                    {
                    }
                    else
                    {
                    }
                };
                Console.WriteLine("daxiaostring" + txt.Length, DateTime.Now.ToLongTimeString());
                _socket.SendAsync(sendArg);
            }
        }
        public static void SendCommand(byte[] txt)     // 发送函数    发送byte[]数组
        {
            try
            {
                if (_socket != null && _socket.Connected)
                {
                    SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                    sendArg.SetBuffer(txt, 0, txt.Length);
                    sendArg.Completed += (objSender, mArg) =>
                    {
                        if (mArg.SocketError == SocketError.Success)
                        {          
                        }
                        else
                        {
                        }
                    };
                    Console.WriteLine("daxiaobyte"+txt.Length, DateTime.Now.ToLongTimeString());
                    _socket.SendAsync(sendArg);
                }
            }catch(Exception e){
                
            }
            
        }
        public void SendCommand(int txt)     // 发送函数   发送大小
        {
            if (_socket != null && _socket.Connected)
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
                };
                Console.WriteLine("daxiao" + buffer.Length, DateTime.Now.ToLongTimeString());
                _socket.SendAsync(sendArg);
            }
        }
        public void Leave_Out()
        {
            if (Server_PC.mySocket != null)
            {
                Server_PC.mySocket.Close();
                //Server_PC._socket.Close();
            }
            Server_PC.mySocket = null;
            Server_PC._socket = null;
        }
    }
}
