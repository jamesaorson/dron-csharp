using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
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
            dynamic value;
            switch (Type.GetTypeCode(propertyType))
            {
                case TypeCode.Byte:
                    value = (byte)node.Value;
                    break;
                case TypeCode.SByte:
                    value = (sbyte)node.Value;
                    break;
                case TypeCode.UInt16:
                    value = (ushort)node.Value;
                    break;
                case TypeCode.Int16:
                    value = (short)node.Value;
                    break;
                case TypeCode.UInt32:
                    value = (uint)node.Value;
                    break;
                case TypeCode.Int32:
                    value = (int)node.Value;
                    break;
                case TypeCode.UInt64:
                    value = (ulong)node.Value;
                    break;
                case TypeCode.Int64:
                    value = (long)node.Value;
                    break;
                case TypeCode.Single:
                    value = (float)node.Value;
                    break;
                case TypeCode.Double:
                    value = (double)node.Value;
                    break;
                case TypeCode.Decimal:
                    value = (decimal)node.Value;
                    break;
                case TypeCode.Object:
                    value = node.Value;
                    break;
                default:
                    throw new Exception($"Unsupported numeric type '{property.PropertyType.Name}");
            }
            if (obj is not null)
            {
                property?.SetValue(obj, value);
            }
            return value;
        }
        #endregion

        #endregion
    }
}