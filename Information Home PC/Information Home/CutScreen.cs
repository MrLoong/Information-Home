using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Home_PC
{
    class CutScreen
    {
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
        public static byte[] Cut_Screen()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            Bitmap bmp = new Bitmap(width, height);
            Image myimage = bmp;
            Graphics g = Graphics.FromImage(myimage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height));
            //获取鼠标
            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
            GetCursorInfo(out pci);
            System.Windows.Forms.Cursor cur = new System.Windows.Forms.Cursor(pci.hCursor);
            cur.Draw(g, new Rectangle(pci.ptScreenPos.x - 10, pci.ptScreenPos.y - 10, cur.Size.Width, cur.Size.Height));
            Image cutImage = ChangeImageWithByte.GetThumbnail(myimage);                   //转换大小
            byte[] imageByte = ChangeImageWithByte.ImgToByt(cutImage);
            return imageByte;
        }
    }

}
