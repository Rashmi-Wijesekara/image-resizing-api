using System.Web.Http;
using ImageMagick;
using System;

namespace ImageResizingAPI.Controllers
{
    public class ResizeMagickController : ApiController
    {
        // GET api/ResizeMagick?width=128&height=97
        public string Get(int width, int height)
        {
            string resizedImagePath = "C:/Users/Dell/Desktop/resized/"+ width + "x" + height + ".jpg";
            using (var image = new MagickImage("C:/Users/Dell/Desktop/certificate.jpg"))
            {
                //var size = new MagickGeometry(width, height);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                //size.IgnoreAspectRatio = true;

                image.Resize(width, height);

                // Save the result
                image.Write(resizedImagePath);
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(@resizedImagePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            return "data:image/jpg;base64," + base64ImageRepresentation;
        }
    }
}
