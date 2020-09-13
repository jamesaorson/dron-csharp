namespace DRON.Tokens
{
    internal class IdentifierToken : Token
    {
        #region Public

        #region Constructors
        internal IdentifierToken(string value)
            : base (TokenKind.Identifier)
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