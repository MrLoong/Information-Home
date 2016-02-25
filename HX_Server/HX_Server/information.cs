using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HX_Server
{
    public class information
    {
        public string ip;
        public string name;
        
        public information()
        {

        }

        public information(string _name, string IP)
        {
            //clthread = _thread;
            ip = IP;
            name = _name;

        }

        public string getHost()
        {
            return ip;
        }

        public string getName()
        {
            return name;
        }


        /*public Thread CLThread
        {
            get { return clthread; }
            set { clthread = value; }
        }*/

        public string  Host
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
