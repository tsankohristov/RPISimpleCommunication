using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;

namespace DataSenderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                SendSampleData();
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void SendSampleData()
        {
            try {

                var sampleData = new
                {
                    value = "Sending some test info" + DateTime.Now.ToString()
                };

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var data = string.Format("Sending data on {0}", DateTime.Now.ToLongTimeString());

                var jsonData = string.Format("{{'sender': '{0}', 'message': '{1}' }}", Environment.MachineName, data);

                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;
                response = httpClient.PostAsync(ConfigurationManager.AppSettings["serverAdress"] + "/api/data", stringContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post succesful, StatusCode: " + response.StatusCode);
                }
                else
                {
                    Console.WriteLine("Post failed, StatusCode: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }
        }
    }
}
