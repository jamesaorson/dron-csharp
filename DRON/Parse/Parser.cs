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
        internal DronNode Parse(IEnumerable<Token> tokens)
        {
            _tokenEnumerator = tokens.GetEnumerator();
            return ParseNode();
        }
        #endregion

        #endregion

        #region Private

        #region Members
        private Token _currentToken => _tokenEnumerator.Current;
        private IEnumerator<Token> _tokenEnumerator { get; set; }
        #endregion

        #region Member Methods
        private DronNode ParseNode()
        {
            DronNode node = null;
            var count = 0;
            while (_tokenEnumerator.MoveNext())
            {
                Console.Write($"{_currentToken.Kind} ");
                switch (_currentToken)
                {
                    case NumberToken token:
                        Console.WriteLine(token.Value);
                        break;
                    case ObjectRefIdentifierToken token:
                        Console.WriteLine(token.Value);
                        break;
                    case QuotedIdentifierToken token:
                        Console.WriteLine(token.Value);
                        break;
                    case IdentifierToken token:
                        Console.WriteLine(token.Value);
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
                count++;
            }
            return node;
        }
        #endregion

        #endregion
    }
}
