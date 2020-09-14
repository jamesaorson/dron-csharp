namespace DRON.Tokens
{
    internal class IdentifierToken : ValueToken
    {
        #region Public

        #region Constructors
        internal IdentifierToken(string value)
            : base (TokenKind.Identifier, value) {}
        #endregion

        #endregion
    }
}