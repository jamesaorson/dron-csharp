namespace DRON.Lex
{
    internal partial class Lexer
    {
        #region Internal

        #region Enums
        internal enum States
        {
            Done,
            Identifier,
            NegativeNumber,
            Number,
            FloatingNumberStart,
            FloatingNumber,
            ObjectRefIdentifier,
            QuotedIdentifier,
            Start,
        }
        #endregion

        #endregion
    }
}