using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace nugetfun
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CreateMonoRootCerts();
            var client = new HttpClient();
            var rsp = await client.GetAsync("https://reqres.in/api/users/2");
            Console.WriteLine("http response status code: " + rsp.StatusCode);
        }

        static void CreateMonoRootCerts(){
            var isMono = Type.GetType("Mono.Runtime") != null;
            if (!isMono) return;

            // ~/.config
            var configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var trustDir = Path.Combine(configDir, ".mono/new-certs/Trust");
            if (!Directory.Exists(trustDir))
            {
                Console.WriteLine("creating: " + trustDir);
                Directory.CreateDirectory(trustDir);
            }

            var assembly = Assembly.GetExecutingAssembly();
            assembly.GetManifestResourceNames()
                .Where(nm => nm.EndsWith(".0"))
                .ToList().ForEach(name =>
                {
                    var file = name.Substring(name.IndexOf('.') + 1);
                    var filename = Path.Combine(trustDir, file);
                    if(!File.Exists(filename)){
                        Console.WriteLine("creating: " + filename);
                        using var fout = File.OpenWrite(filename);
                        using var crt = assembly.GetManifestResourceStream(name);
                        crt.CopyTo(fout);
                    }
                });
        }
    }
}
