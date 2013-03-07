using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace SeaScanUAV
{
    public class ImageUtils
    {
        public static Bitmap ResizeImage(Bitmap imgToResize, int maxDimension)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)maxDimension / (float)sourceWidth);
            nPercentH = ((float)maxDimension / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }

        public static string Base64EncodeImage(Bitmap image)
        {
           string imageBase64 = "";
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                    imageBase64 = Convert.ToBase64String(imageBytes);
                }
            }
            catch (IOException ioe)
            {
                imageBase64 = "";
            }

            return imageBase64;
        }
    }
}
