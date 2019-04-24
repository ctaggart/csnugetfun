using System;
using System.IO;
using System.Reflection;

namespace nugetfun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var location =Assembly.GetExecutingAssembly().Location;
            var dir = Path.GetFullPath(Path.Combine(location, "../../../../../"));
            Console.WriteLine(dir);
            var repo = new LibGit2Sharp.Repository(dir);
            Console.WriteLine(repo.Head.Tip.Sha);
        }
    }
}
