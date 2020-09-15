using System;

namespace DRON.Tokens
{
    internal class IntegralNumberToken : ValueToken
    {
        #region Public

        #region Constructors
        internal IntegralNumberToken(string value)
            : base (TokenKind.IntegralNumber, value) {}
        #endregion

        #region Members
        internal long? NumericValue
            => Value is null ? null : Convert.ToInt64(Value);
        #endregion

        #endregion
    }
}