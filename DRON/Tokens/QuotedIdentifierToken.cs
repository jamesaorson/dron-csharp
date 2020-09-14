namespace DRON.Tokens
{
    internal class QuotedIdentifierToken : IdentifierToken
    {
        #region Public

        #region Constructors
        internal QuotedIdentifierToken(string value)
            : base (value)
        {
            Kind = TokenKind.QuotedIdentifier;
        }
        #endregion

        #region Members
        public string UnquotedValue => Value is null
            ? null : Value.Substring(1, Value.Length - 2);
        #endregion

        #endregion
    }
}