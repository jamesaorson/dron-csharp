namespace DRON.Tokens
{
    internal class ObjectRefIdentifierToken : IdentifierToken
    {
        #region Public

        #region Constructors
        internal ObjectRefIdentifierToken(string value)
            : base (value)
        {
            Kind = TokenKind.ObjectRefIdentifier;
        }
        #endregion

        #region Members
        public string IdValue => Value is null
            ? null : Value.Substring(1);
        #endregion

        #endregion
    }
}