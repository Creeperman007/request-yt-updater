using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;

namespace app_updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0} [INFO] Starting updater...", DateTime.Now.ToString("H:mm:ss"));
            string baseUrl = "https://github.com/Creeperman007/requests-yt/releases/download/v" + Version() + "/RequestYT-Setup.exe";
            try
            {
                Console.WriteLine("{0} [INFO] Downloading file...", DateTime.Now.ToString("H:mm:ss"));
                using (var client = new WebClient())
                {
                    if (!Directory.Exists(@"temp/"))
                    {
                        Directory.CreateDirectory(@"temp/");
                    }
                    client.DownloadFile(baseUrl, @"temp/RequestYT-Setup.exe");
                }
            }
            catch
            {
                Console.WriteLine("{0} [ERROR] Cannot download file.\nStopping.\nPress any key to continue...", DateTime.Now.ToString("H:mm:ss"));
                Console.ReadKey();
                Environment.Exit(0);
            }
            try
            {
                Console.WriteLine("{0} [INFO] Starting installer...", DateTime.Now.ToString("H:mm:ss"));
                Process.Start(@"temp\RequestYT-Setup.exe");
                Console.WriteLine("{0} [INFO] Exiting updater.", DateTime.Now.ToString("H:mm:ss"));
                Environment.Exit(0);
            }
            catch
            {
                Console.WriteLine("{0} [ERROR] Cannot start installer.", DateTime.Now.ToString("H:mm:ss"));
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.ReadKey();
        }
        static string Version()
        {
            try
            {
                WebClient webClient = new WebClient();
                WebClient n = new WebClient();
                var json = n.DownloadString("http://apis.creeperman007.tk/request-yt/v1/");
                string valueOriginal = Convert.ToString(json);
                JObject data = JObject.Parse(valueOriginal);
                return Convert.ToString(data["latestVersion"]);
            }
            catch
            {
                Console.WriteLine("Cannot get latest version.\nPress any key to continue...");
                Environment.Exit(0);
                return "";
            }
        }
    }
}
