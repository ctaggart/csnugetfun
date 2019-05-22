using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace nugetfun
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var rsp = await client.GetAsync("https://reqres.in/api/users/2");
            Console.WriteLine("http response status code: " + rsp.StatusCode);
        }
    }
}
