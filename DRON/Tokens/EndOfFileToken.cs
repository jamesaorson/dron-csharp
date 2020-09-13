namespace DRON.Tokens
{
    internal class EndOfFileToken : Token
    {
        #region Public

        #region Constructors
        internal EndOfFileToken()
            : base (TokenKind.EndOfFile) {}
        #endregion

        #endregion
    }
}