using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DRON.Lex;
using DRON.Tokens;

namespace DRON
{
    public static class Dron
    {
        #region Public

        #region Static Methods
        public static bool Parse([NotNull] string dronString)
            => ParseAsync(dronString).Result;
        
        public async static Task<bool> ParseAsync([NotNull] string dronString)
            => await ParseAsync(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(dronString ?? "")
                )
            );

        public static bool Parse([NotNull] Stream stream)
            => ParseAsync(stream).Result;
        
        public async static Task<bool> ParseAsync([NotNull] Stream stream)
        {
            return await Task.Run<bool>(() =>
            {
                var lexer = new Lexer();
                var count = 0;
                foreach (var token in lexer.Lex(stream))
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
                return true;
            });
        }
        #endregion

        #endregion
    }
}