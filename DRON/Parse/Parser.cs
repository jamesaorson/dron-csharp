using System.Collections.Generic;
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
                        throw new DronUnexpectedTokenException(typeof(DronAttribute), _currentToken);
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
                        throw new DronUnexpectedEOFException(typeof(DronAttribute));
                    }
                    Chomp();
                    return new DronAttribute(name, value);
                default:
                    throw new DronUnexpectedTokenException(typeof(DronAttribute), _currentToken);
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
                TokenKind.OpenBrace or TokenKind.OpenParenthese => ParseObject(),
                TokenKind.OpenBracket => ParseList(),
                _ => throw new DronUnexpectedTokenException(typeof(DronNode), _currentToken),
            };
        
        private DronFalse ParseFalse()
        {
            Chomp();
            return new DronFalse();
        }
        
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
                        throw new DronUnexpectedTokenException(typeof(DronField), _currentToken);
                    }
                    DronNode value = ParseNode();
                    TryChomp(TokenKind.Comma);
                    return new DronField(attributes, name, value);
                default:
                    throw new DronUnexpectedTokenException(typeof(DronField), _currentToken);
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
                    throw new DronDuplicateFieldKeyException(field.Name);
                }
                fields[field.Name] = field;
            }
            return fields;
        }

        private DronNode ParseListItem() => ParseNode();
        
        private DronList ParseList()
        {
            var attributes = ParseAttributes();
            if (!TryChomp(TokenKind.OpenBracket))
            {
                throw new DronUnexpectedTokenException(typeof(DronList), _currentToken);
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
                    throw new DronUnexpectedTokenException(typeof(DronList), _currentToken);
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
                throw new DronUnexpectedTokenException(typeof(DronList), _currentToken);
            }
            return new DronList(attributes, items);
        }

        private DronNode ParseNode()
        {
            return _currentToken.Kind switch
            {
                TokenKind.OpenBrace or TokenKind.OpenParenthese => ParseObject(),
                TokenKind.OpenBracket => ParseList(),
                TokenKind.ObjectRefIdentifier => ParseObjectRef(),
                TokenKind.QuotedIdentifier => ParseQuotedIdentifier(),
                TokenKind.FloatingNumber => ParseFloatingNumber(),
                TokenKind.IntegralNumber => ParseIntegralNumber(),
                TokenKind.Null => ParseNull(),
                TokenKind.True => ParseTrue(),
                TokenKind.False => ParseFalse(),
                _ => throw new DronUnexpectedTokenException(typeof(DronNode), _currentToken),
            };
        }

        private DronNull ParseNull()
        {
            Chomp();
            return new DronNull();
        }

        private DronFloatingNumber ParseFloatingNumber()
        {
            switch (_currentToken.Kind)
            {
                case TokenKind.FloatingNumber:
                    var number = new DronFloatingNumber(
                        (_currentToken as FloatingNumberToken).NumericValue.Value
                    );
                    Chomp();
                    return number;
                default:
                    throw new DronUnexpectedTokenException(typeof(DronFloatingNumber), _currentToken);
            }
        }

        private DronIntegralNumber ParseIntegralNumber()
        {
            switch (_currentToken.Kind)
            {
                case TokenKind.IntegralNumber:
                    var number = new DronIntegralNumber(
                        (_currentToken as IntegralNumberToken).NumericValue.Value
                    );
                    Chomp();
                    return number;
                default:
                    throw new DronUnexpectedTokenException(typeof(DronIntegralNumber), _currentToken);
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
                        throw new DronUnexpectedEOFException(typeof(DronObject));
                    }
                    return new DronObject(attributes, fields);
                default:
                    throw new DronUnexpectedTokenException(typeof(DronObject), _currentToken);
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

        private DronTrue ParseTrue()
        {
            Chomp();
            return new DronTrue();
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
