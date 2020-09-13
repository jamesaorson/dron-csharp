namespace DRON.Tokens
{
    public class NumberToken : Token
    {
        #region Public

        #region Constructors
        public NumberToken(string value)
            : base (TokenKind.Number)
        {
            Value = value;
        }
        #endregion

        #region Members
        public double? NumericValue
            => Value is null ? null : System.Convert.ToDouble(Value);
        public readonly string Value;
        #endregion

        #endregion
    }
}