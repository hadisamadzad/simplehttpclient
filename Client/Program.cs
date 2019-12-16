using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.1.2/api/w/"),
                //Timeout = new TimeSpan(0, 0, 4)
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var eid = "a3wk8a65l";

            var succeeded = 0;
            var failed = 0;

            Task a = new Task(() => { });
            for (int i = 0; i < 200; i++)
            {
                a = new Task(async () =>
                {
                    var get = await httpClient.GetAsync($"courses/{eid}");
                    Console.WriteLine($"result-{i.ToString("00")}: {get.StatusCode}-{get.IsSuccessStatusCode}");
                    if (get.IsSuccessStatusCode)
                        succeeded++;
                    else
                        failed++;
                });
                a.Start();
            }
            
            Console.WriteLine($"Succeeded: {succeeded}");
            Console.WriteLine($"Failed: {failed}");
            Console.ReadKey();
        }
    }
}
