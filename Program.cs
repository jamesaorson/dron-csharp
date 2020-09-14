using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class Example
{
    public class Nested
    {
        public int NestedNum { get; set; }
    }
    public string AnotherNumber { get; set; }
    public double SomeNumber { get; set; }
    public string Id { get; set; }
    public Nested NestedObject { get; set; }
    public IList<object> Things { get; set; }
    public string Null { get; set; }
}

public class Record
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public string Name { get; set; }
    public IEnumerable<object> Numbers { get; set; }
    public double Ratio { get; set; }
    public bool IsValid { get; set; }
    public Record Child { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Dron Sync:  "); Run();
        // Console.Write("Json Sync:  "); RunJson();
        // Console.WriteLine();
        // Console.Write("Dron Async: "); RunAsync();
        // Console.Write("Json Async: "); RunJsonAsync();
    }

    private const int TIMES = 1_000;
    private const string FILE_PREFIX = "record";
    private const string DRON_FILE = FILE_PREFIX + ".dron";
    private const string JSON_FILE = FILE_PREFIX + ".json";
    private static void Run()
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < TIMES; ++i)
        {
            var record = DRON.Dron.Deserialize<Record>(
                File.ReadAllText(DRON_FILE)
            );
            Console.WriteLine(nameof(record));
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }

    private static void RunJson()
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < TIMES; ++i)
        {
            JsonSerializer.Deserialize<Example>(File.ReadAllText(JSON_FILE));
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
                    () => DRON.Dron.DeserializeAsync<Record>(
                        File.OpenRead(DRON_FILE)
                    )
                )
            );
        }
        Task.WaitAll(tasks);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }

    private static void RunJsonAsync()
    {
        var tasks = new Task[TIMES];
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < TIMES; ++i)
        {
            tasks[i] = (
                Task.Run(
                    () => JsonSerializer.DeserializeAsync<Example>(
                        File.OpenRead(JSON_FILE)
                    )
                )
            );
        }
        Task.WaitAll(tasks);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
}
