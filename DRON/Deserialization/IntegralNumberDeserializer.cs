using System;
using System.Collections.Generic;
using System.Reflection;
using DRON.Exceptions;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class IntegralNumberDeserializer : DeserializerBase<DronIntegralNumber>
        
    {
        #region Internal

        #region Constructors
        internal IntegralNumberDeserializer(Deserializer deserializer)
            : base(deserializer) {}
        #endregion

        #region Member Methods
        internal override object Deserialize(
            DronIntegralNumber node,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null,
            IReadOnlyList<DronAttribute> additionalAttributes = null
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

        internal object Deserialize(
            DronFloatingNumber node,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null,
            IReadOnlyList<DronAttribute> additionalAttributes = null
        ) => Deserialize(
            new DronIntegralNumber(
                Convert.ToInt64(node.Value)
            ),
            property,
            obj,
            typeOverride
        );
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
                TypeCode.Object => number,
                _ => throw new DronUnsupportedIntegralNumberTypeException(number.GetType()),
            };
        
        internal static bool IsConvertible(PropertyInfo property)
            => Type.GetTypeCode(property.PropertyType) switch
            {
                (
                    TypeCode.Byte
                    or TypeCode.SByte
                    or TypeCode.UInt16
                    or TypeCode.Int16
                    or TypeCode.UInt32
                    or TypeCode.Int32
                    or TypeCode.UInt64
                    or TypeCode.Int64
                ) => true,
                _ => false,
            };
        #endregion

        #endregion
    }
}