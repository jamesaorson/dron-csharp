using System;
using System.Collections.Generic;
using DRON.Lex;
using DRON.Parse.Exceptions;
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
            return ParseDron();
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
        private bool Chomp()
            => _tokenEnumerator.MoveNext();

        private DronAttribute ParseAttribute()
        {
            switch (_currentToken.Kind)
            {
                case TokenKind.OpenParenthese:
                    Chomp();
                    if (!TryMatch(TokenKind.Identifier))
                    {
                        throw new UnexpectedTokenException(typeof(DronAttribute), _currentToken);
                    }
                    string name = (_currentToken as ValueToken).Value;
                    string value = null;
                    Chomp();
                    if (
                        TryMatch(TokenKind.Identifier)
                        || TryMatch(TokenKind.QuotedIdentifier)
                        || TryMatch(TokenKind.ObjectRefIdentifier)
                    )
                    {
                        value = (_currentToken as ValueToken).Value;
                        Chomp();
                    }
                    if (!TryMatch(TokenKind.CloseParenthese))
                    {
                        throw new EOFException(typeof(DronAttribute));
                    }
                    Chomp();
                    return new DronAttribute(name, value);
                default:
                    throw new UnexpectedTokenException(typeof(DronAttribute), _currentToken);
            }
        }

        private IReadOnlyList<DronAttribute> ParseAttributes()
        {
            var attributes = new List<DronAttribute>();
            while (TryMatch(TokenKind.OpenParenthese))
            {
                attributes.Add(ParseAttribute());
            }
            return attributes;
        }

        private DronNode ParseDron()
            => _currentToken is null ? null : _currentToken.Kind switch {
                TokenKind.OpenParenthese => ParseObject(),
                TokenKind.OpenBrace => ParseObject(),
                TokenKind.OpenBracket => ParseList(),
                _ => throw new UnexpectedTokenException(typeof(DronNode), _currentToken),
            };
        
        private DronField ParseField()
        {
            var attributes = ParseAttributes();
            switch (_currentToken)
            {
                case IdentifierToken token:
                    string name = token.Value;
                    Chomp();
                    if (!TryChomp(TokenKind.Colon))
                    {
                        throw new UnexpectedTokenException(typeof(DronField), _currentToken);
                    }
                    DronNode value = ParseNode();
                    TryChomp(TokenKind.Comma);
                    return new DronField(attributes, name, value);
                default:
                    throw new UnexpectedTokenException(typeof(DronField), _currentToken);
            }
        }

        private IReadOnlyDictionary<string, DronField> ParseFields()
        {
            var fields = new Dictionary<string, DronField>();
            while (!TryMatch(TokenKind.CloseBrace))
            {
                var field = ParseField();
                if (fields.ContainsKey(field.Name))
                {
                    throw new DuplicateFieldKeyException(field.Name);
                }
                fields[field.Name] = field;
            }
            return fields;
        }

        private DronNode ParseListItem() => ParseNode();
        
        private DronList ParseList()
        {
            if (!TryChomp(TokenKind.OpenBracket))
            {
                throw new UnexpectedTokenException(typeof(DronList), _currentToken);
            }
            var items = new List<DronNode>();
            if (!TryMatch(TokenKind.CloseBracket))
            {
                items.Add(ParseListItem());
            }
            while (!TryMatch(TokenKind.CloseBracket))
            {
                if (!TryChomp(TokenKind.Comma))
                {
                    throw new UnexpectedTokenException(typeof(DronList), _currentToken);
                }
                if (TryMatch(TokenKind.CloseBracket))
                {
                    continue;
                }
                items.Add(ParseListItem());
            }
            if (items.Count != 0)
            {
                TryChomp(TokenKind.Comma);
            }
            if (!TryChomp(TokenKind.CloseBracket))
            {
                throw new UnexpectedTokenException(typeof(DronList), _currentToken);
            }
            return new DronList(items);
        }

        private DronNode ParseNode()
        {
            return _currentToken.Kind switch
            {
                TokenKind.OpenBrace => ParseObject(),
                TokenKind.OpenBracket => ParseList(),
                TokenKind.OpenParenthese => ParseObject(),
                TokenKind.ObjectRefIdentifier => ParseObjectRef(),
                TokenKind.QuotedIdentifier => ParseQuotedIdentifier(),
                TokenKind.Number => ParseNumber(),
                TokenKind.Null => ParseNull(),
                _ => throw new UnexpectedTokenException(typeof(DronNode), _currentToken),
            };
        }

        private DronNull ParseNull()
        {
            Chomp();
            return new DronNull();
        }

        private DronNumber ParseNumber()
        {
            switch (_currentToken.Kind)
            {
                case TokenKind.Number:
                    var number = new DronNumber(
                        (_currentToken as NumberToken).NumericValue.Value
                    );
                    Chomp();
                    return number;
                default:
                    throw new UnexpectedTokenException(typeof(DronNumber), _currentToken);
            }
        }

        private DronObject ParseObject()
        {
            var attributes = ParseAttributes();
            switch (_currentToken.Kind)
            {
                case TokenKind.OpenBrace:
                    Chomp();
                    var fields = ParseFields();
                    if (!TryChomp(TokenKind.CloseBrace))
                    {
                        throw new EOFException(typeof(DronObject));
                    }
                    return new DronObject(attributes, fields);
                default:
                    throw new UnexpectedTokenException(typeof(DronObject), _currentToken);
            }
        }

        private DronObjectRef ParseObjectRef()
        {
            var objectRef = new DronObjectRef(
                (_currentToken as ObjectRefIdentifierToken).IdValue
            );
            Chomp();
            return objectRef;
        }

        private DronString ParseQuotedIdentifier()
        {
            var quotedIdentifier = new DronString(
                (_currentToken as QuotedIdentifierToken).UnquotedValue
            );
            Chomp();
            return quotedIdentifier;
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
