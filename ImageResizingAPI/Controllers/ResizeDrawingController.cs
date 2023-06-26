using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Http;

namespace ImageResizingAPI.Controllers
{
    public class ResizeDrawingController : ApiController
    {
        // GET api/ResizeDrawing?width=128&height=97
        public string Get(int width, int height)
        {
            string resizedImagePath = "C:/Users/Dell/Desktop/resized/" + width + "x" + height + ".jpg";

            byte[] resizedImageArray = this.getResizedImage("C:/Users/Dell/Desktop/certificate.jpg", resizedImagePath, width, height);
            string base64ImageRepresentation = Convert.ToBase64String(resizedImageArray);

            return "data:image/jpg;base64," + base64ImageRepresentation;
        }

        private byte[] getResizedImage(String path, String resizedImagePath, int width, int height)
        {
            Bitmap imgIn = new Bitmap(path);
            double y = imgIn.Height;
            double x = imgIn.Width;

            double factor = 1;
            if (width > 0)
            {
                factor = width / x;
            }
            else if (height > 0)
            {
                factor = height / y;
            }
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();
            Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));

            // Set DPI of image (xDpi, yDpi)
            imgOut.SetResolution(72, 72);

            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)),
              new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, getImageFormat(path));

            // save image in folder
            imgOut.Save(resizedImagePath);

            return outStream.ToArray();
        }

        private ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
    }
}
