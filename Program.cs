using System;
using System.IO;
using DRON;
using DRON.Tokens;

class Program
{
    static void Main(string[] args)
    {
        var lexer = new Lexer(File.OpenRead("example.dron"));
        var count = 0;
        foreach (var token in lexer.Lex())
        {
            Console.Write($"{token.Kind} ");
            switch (token)
            {
                case NumberToken t:
                    Console.WriteLine(t.Value);
                    break;
                case ObjectRefIdentifierToken t:
                    Console.WriteLine(t.Value);
                    break;
                case QuotedIdentifierToken t:
                    Console.WriteLine(t.Value);
                    break;
                case IdentifierToken t:
                    Console.WriteLine(t.Value);
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
            count++;
        }
        Console.WriteLine(count);
    }
}
