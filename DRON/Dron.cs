using System;
using System.IO;
using DRON.Lex;
using DRON.Tokens;

namespace DRON
{
    public static class Dron
    {
        #region Public

        #region Static Methods
        public static void Parse(string dronString)
            => Parse(new Lexer(dronString));

        public static void Parse(Stream stream)
            => Parse(new Lexer(stream));
        #endregion

        #endregion

        #region Private

        #region Static Methods
        private static void Parse(Lexer lexer)
        {
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
        #endregion

        #endregion
    }
}