using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Information_Home_PC
{
    class AutoRun
    {
        public static void AntoRunFromAddress(string address)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.SetValue("autorun", @address);
            rk2.Close();
            rk.Close();
        }
    }
}
