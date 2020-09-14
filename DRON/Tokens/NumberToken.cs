namespace DRON.Tokens
{
    internal class NumberToken : ValueToken
    {
        #region Public

        #region Constructors
        internal NumberToken(string value)
            : base (TokenKind.Number, value) {}
        #endregion

        #region Members
        internal double? NumericValue
            => Value is null ? null : System.Convert.ToDouble(Value);
        #endregion

        #endregion
    }
}