using System;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class IntegralNumberSerializer : SerializerBase<DronIntegralNumber, object>
    {
        #region Internal

        #region Member Methods
        internal override DronIntegralNumber Serialize(object number)
            => new DronIntegralNumber(ConvertToIntegralNumber(number));
        #endregion

        #region Static Methods
        internal static long ConvertToIntegralNumber(object number)
            => IsConvertible(number)
                ? Convert.ToInt64(number)
                : throw new Exception($"Unsupported integral type '{number.GetType().Name}'");
        
        internal static bool IsConvertible(object number)
            => number switch
            {
                (
                    Byte
                    or SByte
                    or UInt16
                    or Int16
                    or UInt32
                    or Int32
                    or UInt64
                    or Int64
                 ) => true,
                _ => false,
            };
        #endregion

        #endregion
    }
}