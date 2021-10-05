using System;
using System.IO;
using System.Linq;
using System.Net;
using KMChartsUpdater.BLL.Utils;

namespace KMChartsUpdater.BLL.Infrastructure
{
    class ImgSaver
    {
        private static WebClient _webClient;

        static ImgSaver()
        {
            _webClient = new WebClient();
        }

        public ImgSaver()
        {
             
        }

        public static string SaveFromUrl(string path, string url)
        {
            string ext;

            if (url == null)
                return "/Uploads/no-image.png";

            if (url.Contains(".png"))
                ext = "png";
            else
                ext = "jpeg";

            ext = ext.Split('?').First();

            string randomName = RandomString.Generate(10);
            string fileName = $"{randomName}_{DateTime.Now:hh-mm-ss}.{ext}";
            string fullPath = Directory.GetCurrentDirectory() + path + fileName;

            try
            {
                _webClient.DownloadFile(url, fullPath);
            }
            catch(Exception)
            {
                return "/Uploads/no-image.png";
            }

            return path + fileName;
        }
    }
}
