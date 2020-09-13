using System;
using System.Collections.Generic;
using DRON.Lex;
using DRON.Tokens;

namespace DRON.Parse
{
    internal class Parser
    {
        #region Internal

        #region Member Methods
        internal DronObject Parse(IEnumerable<Token> tokens)
        {
            DronObject ast = null;
            var count = 0;
            foreach (var token in tokens)
            {
                // Console.Write($"{token.Kind} ");
                switch (token)
                {
                    case NumberToken t:
                        // Console.WriteLine(t.Value);
                        break;
                    case ObjectRefIdentifierToken t:
                        // Console.WriteLine(t.Value);
                        break;
                    case QuotedIdentifierToken t:
                        // Console.WriteLine(t.Value);
                        break;
                    case IdentifierToken t:
                        // Console.WriteLine(t.Value);
                        break;
                    default:
                        // Console.WriteLine();
                        break;
                }
                count++;
            }
            // Console.WriteLine(count);
            return ast;
        }
        #endregion

        #endregion
    }
}
