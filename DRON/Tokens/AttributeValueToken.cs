namespace DRON.Tokens
{
    public class AttributeValueToken : Token
    {
        #region Public

        #region Constructors
        public AttributeValueToken(string value)
            : base (TokenKind.AttributeValue)
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