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
            Chomp();
            return ParseNode();
        }
        #endregion

        #endregion

        #region Private

        #region Members
        private Token _currentToken => _tokenEnumerator.Current;
        private bool _isEOF
            => TryMatch(TokenKind.EndOfFile);
        private IEnumerator<Token> _tokenEnumerator { get; set; }
        #endregion

        #region Member Methods
        private void CheckUnexpectedEOF()
        {
            if (_isEOF)
            {
                throw new Exception("Unexpected EOF");
            }
        }

        private bool Chomp()
            => _tokenEnumerator.MoveNext();

        private DronNode ParseNode()
        {
            DronNode node = null;
            switch (_currentToken.Kind)
            {
                case TokenKind.OpenBrace:
                    while (
                        !_isEOF
                        && !TryMatch(TokenKind.CloseBrace)
                    )
                    {
                        Chomp();
                    }
                    CheckUnexpectedEOF();
                    break;
                case TokenKind.OpenBracket:
                    while (
                        !_isEOF
                        && !TryMatch(TokenKind.CloseBracket)
                    )
                    {
                        Chomp();
                    }
                    CheckUnexpectedEOF();
                    break;
                case TokenKind.OpenParenthese:
                    while (
                        !_isEOF
                        && !TryMatch(TokenKind.CloseParenthese)
                    )
                    {
                        Chomp();
                    }
                    CheckUnexpectedEOF();
                    break;
            }
            return node;
        }

        private bool TryChomp(TokenKind kind)
        {
            if (!TryMatch(kind))
            {
                return false;
            }
            return Chomp();
        }

        private bool TryMatch(TokenKind kind)
            => _currentToken?.Kind == kind;
        #endregion

        #endregion
    }
}
