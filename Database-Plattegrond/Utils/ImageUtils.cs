using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Database_Plattegrond.Utils
{
    public class ImageUtils
    {

        /// <summary>
        /// Rescales an images, preserving the same aspect ratio, to a maxWidth and maxHeight
        /// </summary>
        /// <param name="imageArray">A byte array of the image</param>
        /// <param name="maxWidth">the max width of the new image</param>
        /// <param name="maxHeight">the max height of the new image</param>
        /// <returns>returns a byte array of the new rescaled image</returns>
        public byte[] ScaleImage(byte[] imageArray, int maxWidth, int maxHeight)
        {
            using (MemoryStream memStream = new MemoryStream(imageArray)) 
            {
                var image = Image.FromStream(memStream);

                    var ratioX = (double)maxWidth / image.Width;
                    var ratioY = (double)maxHeight / image.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(image.Width * ratio);
                    var newHeight = (int)(image.Height * ratio);

                    var newImage = new Bitmap(newWidth, newHeight);

                    var graphics = Graphics.FromImage(newImage);
                    graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                    newImage.Save(memStream, ImageFormat.Png);

                    return memStream.ToArray();
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                byte[] a = ms.ToArray();
                ms.Dispose();
                return a;
            }
        }

        public Image ByteArrayToImage(byte[] imageArray)
        {
            using (var ms = new MemoryStream(imageArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}