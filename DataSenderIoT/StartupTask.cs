using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using System.Net.Http.Headers;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace DataSenderIoT
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        private ThreadPoolTimer timer;
        private int counter = 0;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromMilliseconds(1 * 1000));

            SendSampleData();

        }

        private void Timer_Tick(ThreadPoolTimer timer)
        {
            SendSampleData();
            ++counter;
        }

        private static void SendSampleData()
        {
            try
            {
                var sampleData = new
                {
                    value = "Sending some test info" + DateTime.Now.ToString()
                };

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // ToLongTimeString not available
                // var data = string.Format("Sending data on {0}", DateTime.Now.ToLongTimeString());
                var data = string.Format("Sending data on {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                // Environment.MachineName not available
                // var jsonData = string.Format("{{'sender': '{0}', 'message': '{1}' }}", Environment.MachineName, data);
                var jsonData = string.Format("{{'sender': '{0}', 'message': '{1}' }}", "Raspberry PI W10 (winberry)", data);

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                // ConfigurationManager.AppSettings not available
                // response = httpClient.PostAsync(ConfigurationManager.AppSettings["serverAdress"] + "/api/data", stringContent).Result;
                response = httpClient.PostAsync("http://win10dev:9000/api/data", stringContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Console not available
                    //Console.WriteLine("Post succesful, StatusCode: " + response.StatusCode);
                }
                else
                {
                    // Console not available
                    //Console.WriteLine("Post failed, StatusCode: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Console not available
                //Console.Write(ex.Message + ex.StackTrace);
            }
        }
    }
}
