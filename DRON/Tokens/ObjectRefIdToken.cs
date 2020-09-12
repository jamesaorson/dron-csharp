namespace DRON.Tokens
{
    public class ObjectRefIdToken : Token
    {
        #region Public

        #region Constructors
        public ObjectRefIdToken()
            : base (TokenKind.ObjectRefId) {}
        #endregion

        #region Members
        public readonly string Value;
        #endregion

        #endregion
    }
}