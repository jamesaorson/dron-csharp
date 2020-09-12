using System;
using System.IO;
using DRON;

class Program
{
    static void Main(string[] args)
    {
        var lexer = new Lexer(File.OpenRead("example.dron"));
        var count = 0;
        foreach (var token in lexer.Lex())
        {
            Console.WriteLine(token.Kind);
            count++;
        }
        Console.WriteLine(count);
    }
}
