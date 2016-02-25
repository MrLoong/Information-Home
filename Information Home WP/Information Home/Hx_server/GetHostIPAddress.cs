using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Home.Hx_server
{
    class GetHostIPAddress
    {
        public string GetIPAddress()
        {
            string strIPAddress = null;
            List<string> arrayIPAddress = new List<string>();
            // Windows.Networking.Connectivity.  
            var hostNames = Windows.Networking.Connectivity.NetworkInformation.GetHostNames();
            foreach (var hn in hostNames)
            {
                if (hn.IPInformation != null)
                {
                    string ipAddress = hn.DisplayName;
                    arrayIPAddress.Add(ipAddress);
                }
            }
            if (arrayIPAddress.Count < 1)
            {
                return null;
            }
            if (arrayIPAddress.Count == 1)
            {
                strIPAddress = arrayIPAddress[0];
            }
            if (arrayIPAddress.Count > 1)
            {
                strIPAddress = arrayIPAddress[arrayIPAddress.Count - 1];
            }
            // System.Console.WriteLine();  
            for (int i = 0; i < arrayIPAddress.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("No.{0} host IP is: {1}", i + 1, arrayIPAddress[i]);
            }
            return strIPAddress;
        }  
    }
}
