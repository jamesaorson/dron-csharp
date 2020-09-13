namespace DRON
{
    public partial class Lexer
    {
        #region Internal

        #region Enums
        internal enum States
        {
            Done,
            Identifier,
            NumberNegative,
            Number,
            NumberFloatingStart,
            NumberFloating,
            ObjectRefIdentifier,
            QuotedIdentifier,
            Start,
        }
        #endregion

        #endregion
    }
}