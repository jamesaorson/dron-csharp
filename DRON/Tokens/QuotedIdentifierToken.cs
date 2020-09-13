namespace DRON.Tokens
{
    internal class QuotedIdentifierToken : Token
    {
        #region Public

        #region Constructors
        internal QuotedIdentifierToken(string value)
            : base (TokenKind.QuotedIdentifier)
        {
            Value = value;
        }
        #endregion

        #region Members
        internal readonly string Value;
        #endregion

        #endregion
    }
}