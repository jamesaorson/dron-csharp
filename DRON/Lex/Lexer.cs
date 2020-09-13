using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using DRON.Tokens;

namespace DRON.Lex
{
    internal partial class Lexer
    {
        #region Internal

        #region Constructors
        internal Lexer(string dronString)
        {
            _currentStreamValue = -1;
            _stream = new MemoryStream(
                Encoding.UTF8.GetBytes(dronString ?? "")
            );
        }

        internal Lexer([NotNull] Stream stream)
        {
            if (stream is null)
            {
                throw new System.ArgumentNullException("'stream' argument cannot be null");
            }

            _stream = stream;
        }
        #endregion

        #region Member Methods
        internal IEnumerable<Token> Lex()
        {
            if (_stream.Length == 0)
            {
                yield break;
            }
            
            _state = States.Start;
            while (_currentToken?.Kind != TokenKind.EndOfFile)
            {
                yield return GetNextToken();
            }
        }
        #endregion

        #endregion

        #region Protected

        #region Members
        protected int _currentStreamValue { get; set; }
        protected Token _currentToken { get; set; }
        protected string _tokenString { get; set; }
        protected bool _preserveLastValue { get; set; }
        protected Stream _stream { get; set; }
        #endregion

        #region Member Methods
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected Token GetNextToken()
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
                                _state = States.NumberNegative;
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
                                Console.WriteLine($"Unexpected character: '{character}'");
                                SetTokenAndDone(new UnknownToken());
                                continue;
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
                            if (value == "null")
                            {
                                token = new NullToken();
                            }
                            else
                            {
                                token = new IdentifierToken(value);
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
                    case States.NumberNegative:
                        if (!Char.IsDigit(character))
                        {
                            throw new Exception("Expected at least one digit following a negative sign");
                        }
                        _state = States.Number;
                        _preserveLastValue = true;
                        continue;
                    case States.NumberFloatingStart:
                        if (!Char.IsDigit(character))
                        {
                            throw new Exception("Expected at least one digit following a decimal point");
                        }
                        _state = States.NumberFloating;
                        _preserveLastValue = true;
                        continue;
                    case States.Number:
                    case States.NumberFloating:
                        if (character == Alphabet.Decimal)
                        {
                            if (_state == States.NumberFloating)
                            {
                                throw new Exception("Extra decimal found in floating point number");
                            }
                            _state = States.NumberFloatingStart;
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
                            new NumberToken(_tokenString.Trim())
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
        protected void SetTokenAndDone(Token token)
        {
            _currentToken = token;
            _state = States.Done;   
        }
        #endregion

        #endregion

        #region Internal

        #region Members
        internal States _state { get; set; }
        #endregion

        #endregion

        ~Lexer()
        {
            if (_stream is not null)
            {
                _stream.Dispose();
            }
        }
    }
}