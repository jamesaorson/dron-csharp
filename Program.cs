using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var tasks = new List<Task>();
        for (int i = 0; i < 1; ++i)
        {
            tasks.Add(DRON.Dron.ParseAsync(File.OpenRead("small.dron")));
        }
        Task.WaitAll(tasks.ToArray());
        System.Console.WriteLine("Done");
    }
}
