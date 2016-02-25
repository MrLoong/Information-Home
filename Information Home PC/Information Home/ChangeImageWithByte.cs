using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Information_Home_PC
{
    class ChangeImageWithByte
    {
        public static byte[] ImgToByt(Image img)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imagedata = ms.GetBuffer();
            return imagedata;
        }

        public static Image BytToImg(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            Image img = Image.FromStream(ms);
            return img;
        }

        public static byte[] getImageByte(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            return imgByte;
        }

        public static Image BytToImg(byte[] byt, int n)
        {
            //System.Threading.Thread.Sleep(50);
            MemoryStream ms = new MemoryStream();
            ms.Write(byt, 0, n);
            Image img = Image.FromStream(ms);
            return img;
        }
        public static bool ThumbnailCallback()
        {
            return false;
        }
        public static Image GetThumbnail(Image img)           //将截图转成800*480
        {
            Image ResourceImage = img;
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Image thumbnail = ResourceImage.GetThumbnailImage(800, 480, myCallback, IntPtr.Zero);
            ResourceImage.Dispose();
            return thumbnail;
        }
    }
}
