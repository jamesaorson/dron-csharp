using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Run();
        // RunAsync();
    }

    private const int TIMES = 1;
    private static void Run()
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < TIMES; ++i)
        {
            DRON.Dron.Parse(
                File.OpenRead("small.dron")
            );
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
    
    private static void RunAsync()
    {
        var tasks = new Task[TIMES];
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < TIMES; ++i)
        {
            tasks[i] = (
                Task.Run(
                    () => DRON.Dron.ParseAsync(
                        File.OpenRead("small.dron")
                    )
                )
            );
        }
        Task.WaitAll(tasks);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
}
