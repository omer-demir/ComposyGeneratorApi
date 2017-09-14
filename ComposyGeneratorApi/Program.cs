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
            string baseAddress = "http://127.0.0.1:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Api is up with 9000 port");
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
