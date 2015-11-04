using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            StartOptions options = new StartOptions();
            options.Urls.Add("http://localhost:9000");
            options.Urls.Add("http://127.0.0.1:9000");
            options.Urls.Add(string.Format("http://{0}:9000", Environment.MachineName));

            // Start OWIN host 
            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("DataReceiver service running, press any key to exit.");

                Console.ReadLine();
            }

        }
    }
}
