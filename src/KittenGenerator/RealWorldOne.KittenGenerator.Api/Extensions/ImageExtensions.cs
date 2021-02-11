using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RealWorldOne.KittenGenerator.Api.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToBytes(this Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));
            
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);

            return memoryStream.ToArray();  
        }
    }
}