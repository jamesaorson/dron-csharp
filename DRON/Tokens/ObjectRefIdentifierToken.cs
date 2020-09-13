namespace DRON.Tokens
{
    internal class ObjectRefIdentifierToken : Token
    {
        #region Public

        #region Constructors
        internal ObjectRefIdentifierToken(string value)
            : base (TokenKind.ObjectRefIdentifier)
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