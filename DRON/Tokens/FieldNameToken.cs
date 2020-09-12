namespace DRON.Tokens
{
    public class FieldNameToken : Token
    {
        #region Public

        #region Constructors
        public FieldNameToken()
            : base (TokenKind.FieldName) {}
        #endregion

        #region Members
        public readonly string Value;
        #endregion

        #endregion
    }
}