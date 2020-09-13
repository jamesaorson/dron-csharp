namespace DRON.Tokens
{
    public class IdentifierToken : Token
    {
        #region Public

        #region Constructors
        public IdentifierToken(string value)
            : base (TokenKind.Identifier)
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