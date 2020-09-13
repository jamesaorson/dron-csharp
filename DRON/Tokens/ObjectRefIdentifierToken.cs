namespace DRON.Tokens
{
    public class ObjectRefIdentifierToken : Token
    {
        #region Public

        #region Constructors
        public ObjectRefIdentifierToken(string value)
            : base (TokenKind.ObjectRefIdentifier)
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