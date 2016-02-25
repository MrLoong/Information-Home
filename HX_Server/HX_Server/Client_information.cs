using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HX_Server
{
    public class Client_information
    {    
        public Thread clthread;
        public string endpoint;
        public string name;
        public Socket socket;
        
        public Client_information()
        {

        }

        public Client_information(string _name, string _endpoint, Socket _sock)
        {
            //clthread = _thread;
            endpoint = _endpoint;
            name = _name;
            socket = _sock;

        }
        public override string ToString()
        {
            return endpoint.ToString() + " : " + name;
        }

        public string getHost()
        {
            return endpoint;
        }

        public string getName()
        {
            return name;
        }

        public Socket getSocket()
        {
            return socket;
        }

        /*public Thread CLThread
        {
            get { return clthread; }
            set { clthread = value; }
        }*/

        public string  Host
        {
            get { return endpoint; }
            set { endpoint = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Socket Sock
        {
            get { return socket; }
            set { socket = value; }
        }


    }
    

}
