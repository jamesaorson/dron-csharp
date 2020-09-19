using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class FloatingNumberDeserializer : DeserializerBase<DronFloatingNumber>
    {
        #region Internal

        #region Constructors
        internal FloatingNumberDeserializer(Deserializer deserializer)
            : base(deserializer) {}
        #endregion

        #region Member Methods
        internal override object Deserialize(
            DronFloatingNumber node,
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

        internal object Deserialize(
            DronIntegralNumber node,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null
        ) => Deserialize(
            new DronFloatingNumber(
                Convert.ToDouble(node.Value)
            ),
            property,
            obj,
            typeOverride
        );
        #endregion

        #region Static Methods
        internal static object ConvertNumber(object number, Type numberType)
            => Type.GetTypeCode(numberType) switch
            {
                TypeCode.Single => Convert.ToSingle(number),
                TypeCode.Double => Convert.ToDouble(number),
                TypeCode.Decimal => Convert.ToDecimal(number),
                TypeCode.Object => number,
                _ => throw new Exception($"Unsupported floating numeric type '{number.GetType().Name}': {Type.GetTypeCode(numberType)}"),
            };
        
        internal static bool IsConvertible(PropertyInfo property)
            => Type.GetTypeCode(property.PropertyType) switch
            {
                TypeCode.Single or TypeCode.Double or TypeCode.Decimal => true,
                _ => false,
            };
        #endregion

        #endregion
    }
}