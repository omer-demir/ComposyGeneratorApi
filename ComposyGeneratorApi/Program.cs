using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ComposyGeneratorApi
{
    class Program
    {
        static void Main(string[] args)
        {
            StartOptions options = new StartOptions();
            options.Urls.Add("http://localhost:8080");
            options.Urls.Add("http://127.0.0.1:8080");
            options.Urls.Add($"http://{Environment.MachineName}:8080");
            options.Urls.Add("http://52.166.0.186:8080");

            // Start OWIN host 
            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Api is up with 8080 port");
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/generateapi").Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}
