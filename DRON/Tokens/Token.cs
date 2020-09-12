namespace DRON.Tokens
{
    public abstract class Token
    {
        #region Public

        #region Members
        public readonly TokenKind Kind;
        #endregion

        #endregion
        
        #region Protected
        
        #region Constructors
        protected Token(TokenKind kind)
        {
            Kind = kind;
        }
        #endregion

        #endregion
    }
}