using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using DRON.Lex.Exceptions;
using DRON.Tokens;

namespace DRON.Lex
{
    internal partial class Lexer
    {
        #region Internal

        #region Member Methods
        internal IEnumerable<Token> Lex([NotNull] Stream stream)
        {
            if (stream is null)
            {
                throw new System.ArgumentNullException("'stream' cannot be null");
            }
            
            _stream = stream;
            _currentStreamValue = -1;
            _state = States.Start;
            using (_stream)
            {
                if (_stream.Length == 0)
                {
                    yield break;
                }
                while (_currentToken?.Kind != TokenKind.EndOfFile)
                {
                    yield return GetNextToken();
                }
            }
        }
        #endregion

        #endregion

        #region Private

        #region Members
        private int _currentStreamValue;
        private Token _currentToken;
        private string _tokenString;
        private bool _preserveLastValue;
        private Stream _stream;
        #endregion

        #region Member Methods
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private Token GetNextToken()
        {
            if (_currentToken?.Kind == TokenKind.EndOfFile)
            {
                return _currentToken;
            }
            if (_state == States.Done)
            {
                _state = States.Start;
            }
            _tokenString = "";
            if (!_preserveLastValue)
            {
                _currentStreamValue = _stream.ReadByte();
            }
            _preserveLastValue = false;
            var character = (char)_currentStreamValue;
            while (_currentStreamValue != -1)
            {
                switch (_state)
                {
                    case States.Done:
                        return _currentToken;
                    case States.Start:
                        switch (character)
                        {
                            case Alphabet.OpenBrace:
                                SetTokenAndDone(new OpenBraceToken());
                                continue;
                            case Alphabet.CloseBrace:
                                SetTokenAndDone(new CloseBraceToken());
                                continue;
                            case Alphabet.OpenBracket:
                                SetTokenAndDone(new OpenBracketToken());
                                continue;
                            case Alphabet.CloseBracket:
                                SetTokenAndDone(new CloseBracketToken());
                                continue;
                            case Alphabet.OpenParenthese:
                                SetTokenAndDone(new OpenParentheseToken());
                                continue;
                            case Alphabet.CloseParenthese:
                                SetTokenAndDone(new CloseParentheseToken());
                                continue;
                            case Alphabet.Comma:
                                SetTokenAndDone(new CommaToken());
                                continue;
                            case Alphabet.Colon:
                                SetTokenAndDone(new ColonToken());
                                continue;
                            case Alphabet.Quote:
                                _state = States.QuotedIdentifier;
                                break;
                            case Alphabet.At:
                                _state = States.ObjectRefIdentifier;
                                break;
                            case Alphabet.Hyphen:
                                _state = States.NegativeNumber;
                                break;
                            case char c when Char.IsDigit(character):
                                _state = States.Number;
                                break;
                            case char c when Char.IsLetter(c):
                                _state = States.Identifier;
                                break;
                            case char c when Char.IsWhiteSpace(c):
                                break;
                            default:
                                throw new UnexpectedCharacterException(character);
                        }
                        break;
                    case States.Identifier:
                        if (
                            Char.IsWhiteSpace(character)
                            || (
                                character != Alphabet.Underscore
                                && !Char.IsLetterOrDigit(character)
                            )
                        )
                        {
                            var value = _tokenString.Trim();
                            Token token;
                            switch (value)
                            {
                                case "null":
                                   token = new NullToken();
                                   break;
                                case "true":
                                   token = new TrueToken();
                                   break;
                                case "false":
                                   token = new FalseToken();
                                   break;
                                default:
                                    token = new IdentifierToken(value);
                                    break;
                            }
                            SetTokenAndDone(token);
                            
                            _preserveLastValue = true;
                            continue;
                        }
                        break;
                    case States.ObjectRefIdentifier:
                        if (
                            Char.IsWhiteSpace(character)
                            || (
                                character != Alphabet.Underscore
                                && !Char.IsLetterOrDigit(character)
                            )
                        )
                        {
                            SetTokenAndDone(
                                new ObjectRefIdentifierToken(_tokenString.Trim())
                            );
                            _preserveLastValue = true;
                            continue;
                        }
                        break;
                    case States.QuotedIdentifier:
                        if (character == Alphabet.Quote)
                        {
                            SetTokenAndDone(
                                new QuotedIdentifierToken(
                                    (_tokenString + "\"").Trim()
                                )
                            );
                            continue;
                        }
                        break;
                    case States.NegativeNumber:
                        if (!Char.IsDigit(character))
                        {
                            throw new Exception("Expected at least one digit following a negative sign");
                        }
                        _state = States.Number;
                        _preserveLastValue = true;
                        continue;
                    case States.FloatingNumberStart:
                        if (!Char.IsDigit(character))
                        {
                            throw new Exception("Expected at least one digit following a decimal point");
                        }
                        _state = States.FloatingNumber;
                        _preserveLastValue = true;
                        continue;
                    case States.Number:
                    case States.FloatingNumber:
                        if (character == Alphabet.Decimal)
                        {
                            if (_state == States.FloatingNumber)
                            {
                                throw new Exception("Extra decimal found in floating point number");
                            }
                            _state = States.FloatingNumberStart;
                            break;
                        }
                        if (Char.IsLetter(character))
                        {
                            throw new Exception($"Unexpected character in number literal '{character}'");
                        }
                        if (Char.IsDigit(character))
                        {
                            break;
                        }
                        SetTokenAndDone(
                            _state == States.FloatingNumber
                                ? new FloatingNumberToken(_tokenString.Trim())
                                : new IntegralNumberToken(_tokenString.Trim())
                        );
                        _preserveLastValue = true;
                        continue;
                    default:
                        throw new IndexOutOfRangeException($"Invalid enum States '{_state}'");
                }

                _tokenString += character;
                _currentStreamValue = _stream.ReadByte();
                character = (char)_currentStreamValue;
            }
            if (_tokenString.Trim().Length != 0)
            {
                throw new Exception("Garbage");
            }
            _currentToken = new EndOfFileToken();
            return _currentToken;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetTokenAndDone(Token token)
        {
            _currentToken = token;
            _state = States.Done;   
        }
        #endregion

        #endregion

        #region Internal

        #region Members
        internal States _state;
        #endregion

        #endregion
    }
}