using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class NumberDeserializer : DeserializerBase<DronNumber>
    {
        #region Internal

        #region Member Methods
        internal override object Deserialize(
            DronNumber node,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null
        )
        {
            var propertyType = typeOverride is not null ? typeOverride : property?.PropertyType;
            if (propertyType is null)
            {
                return node.Value;
            }
            var value = ConvertNumber(node.Value, propertyType);
            if (obj is not null)
            {
                property?.SetValue(obj, value);
            }
            return value;
        }
        #endregion

        #region Static Methods
        internal static object ConvertNumber(object number, Type numberType)
            => (Type.GetTypeCode(numberType)) switch
            {
                TypeCode.Byte => Convert.ToByte(number),
                TypeCode.SByte => Convert.ToSByte(number),
                TypeCode.UInt16 => Convert.ToUInt16(number),
                TypeCode.Int16 => Convert.ToInt16(number),
                TypeCode.UInt32 => Convert.ToUInt32(number),
                TypeCode.Int32 => Convert.ToInt32(number),
                TypeCode.UInt64 => Convert.ToUInt64(number),
                TypeCode.Int64 => Convert.ToInt64(number),
                TypeCode.Single => Convert.ToSingle(number),
                TypeCode.Double => Convert.ToDouble(number),
                TypeCode.Decimal => Convert.ToDecimal(number),
                TypeCode.Object => number,
                _ => throw new Exception($"Unsupported numeric type '{number.GetType().Name}"),
            };
        #endregion

        #endregion
    }
}