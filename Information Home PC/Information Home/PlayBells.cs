using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Media;
namespace Information_Home_PC
{
    public  class PlayBells
    {
        [DllImport("winmm.dll")]
     
        private static extern int mciSendString(string command, StringBuilder sb, int size, IntPtr callBack);
        private static SoundPlayer soundplayer = new SoundPlayer();


        public static void StartPlayMci(string flyName)
        {
            mciSendString(string.Format("Open {0} alias music", flyName), null, 0, IntPtr.Zero);
            mciSendString("Play music repeat", null, 0, IntPtr.Zero);
        }

        public static void StartPlayMedia(string flyName)
        {
            soundplayer.SoundLocation = flyName;
            soundplayer.PlayLooping();
        }
        public static void StopPlayMedia()
        {
            soundplayer.Stop();
        }

        public static void StopPlayMci()
        {
            mciSendString("Close music", null, 0, IntPtr.Zero);
        }
    }
}