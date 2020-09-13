using System.IO;

class Program
{
    static void Main(string[] args)
    {
        DRON.Dron.Parse(File.OpenRead("small.dron"));
        // DRON.Dron.Parse(File.OpenRead("example.dron"));
    }
}
