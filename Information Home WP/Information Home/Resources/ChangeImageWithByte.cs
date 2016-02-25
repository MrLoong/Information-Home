using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Information_Home.Resources
{
    class ChangeImageWithByte
    {
        public static byte[] ImageToByteArray(BitmapImage imageSource)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap(imageSource);

                // write an image into the stream
                Extensions.SaveJpeg(btmMap, ms,
                    imageSource.PixelWidth, imageSource.PixelHeight, 0, 100);

                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] bits)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(bits))
            {
                bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmapImage.SetSource(ms);
                Image image = new Image();
                image.Source = bitmapImage;
                return image;
            }
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] bitData)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(bitData))
            {
                bitmap.CreateOptions = BitmapCreateOptions.DelayCreation;
                bitmap.SetSource(ms);
                return bitmap;
            }
        }
    }
}
