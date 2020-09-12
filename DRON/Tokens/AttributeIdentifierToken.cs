namespace DRON.Tokens
{
    public class AttributeIdentifierToken : Token
    {
        #region Public

        #region Constructors
        public AttributeIdentifierToken(string value)
            : base (TokenKind.AttributeIdentifier)
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