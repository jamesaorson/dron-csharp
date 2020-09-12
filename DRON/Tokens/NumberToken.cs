namespace DRON.Tokens
{
    public class NumberToken : Token
    {
        #region Public

        #region Constructors
        public NumberToken()
            : base (TokenKind.Number) {}
        #endregion

        #region Members
        public double? NumericValue
            => Value is null ? null : System.Convert.ToDouble(Value);
        public readonly string Value;
        #endregion

        #endregion
    }
}