using System;
using System.Drawing;
using System.Net;

namespace RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage.ImageDownloaders
{
    public class KittenImageDownloader : IDownloadImage
    {
        private readonly Uri _uri;

        public KittenImageDownloader(Uri uri)
        {
            _uri = uri;
        }

        public Image Download()
        {
            using var webClient = new WebClient();
            var stream = webClient.OpenRead(_uri);
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var image = Image.FromStream(stream);

            stream.Flush();
            stream.Close();

            return image;
        }
    }
}