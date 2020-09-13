namespace DRON.Tokens
{
    public class QuotedIdentifierToken : Token
    {
        #region Public

        #region Constructors
        public QuotedIdentifierToken(string value)
            : base (TokenKind.QuotedIdentifier)
        {
            Value = value;
        }
        #endregion

        #region Members
        public readonly string Value;
        #endregion

        #endregion
    }
}