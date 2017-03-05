using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program6
    {
        static void Main6(string[] args)
        {
            Task<string> downloadTask = DownloadWebPageAsync("http://aabsys.com");
            while(!downloadTask.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(250);
            }

            Console.WriteLine(downloadTask.Result);
        }

        public static string DownloadWebPage(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            {
                // this will return the content of the web page.
                return reader.ReadToEnd();
            }
        }

        private static Task<string> DownloadWebPageAsync(string url)
        {
            return Task.Factory.StartNew(() => DownloadWebPage(url));
        }
    }
}
