using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets; 

namespace Information_Home_PC
{
    class StateObject       // //异步Socket中转对象状态
    {
        private const int BUFFER_SIZE = 40960000;
        public byte[] Buffer
        {
            get;
            set;
        }
        public Socket theSocket
        {
            get;
            set;
        }
        public StateObject()
        {
            this.Buffer = new byte[BUFFER_SIZE];
        }
    }
}
