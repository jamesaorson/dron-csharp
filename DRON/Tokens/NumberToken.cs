namespace DRON.Tokens
{
    internal class NumberToken : Token
    {
        #region Public

        #region Constructors
        internal NumberToken(string value)
            : base (TokenKind.Number)
        {
            Value = value;
        }
        #endregion

        #region Members
        internal double? NumericValue
            => Value is null ? null : System.Convert.ToDouble(Value);
        internal readonly string Value;
        #endregion

        #endregion
    }
}