namespace DRON.Tokens
{
    internal class FloatingNumberToken : ValueToken
    {
        #region Public

        #region Constructors
        internal FloatingNumberToken(string value)
            : base (TokenKind.FloatingNumber, value) {}
        #endregion

        #region Members
        internal double? NumericValue
            => Value is null ? null : System.Convert.ToDouble(Value);
        #endregion

        #endregion
    }
}