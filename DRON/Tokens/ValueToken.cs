namespace DRON.Tokens
{
    public abstract class ValueToken : Token
    {
        #region Public

        #region Members
        public readonly string Value;
        #endregion

        #endregion
        
        #region Protected
        
        #region Constructors
        protected ValueToken(TokenKind kind, string value)
            : base(kind)
        {
            Value = value;
        }
        #endregion

        #endregion
    }
}