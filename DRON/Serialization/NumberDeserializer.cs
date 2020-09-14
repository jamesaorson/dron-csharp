using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class NumberDeserializer : DeserializerBase<DronNumber>
    {
        #region Internal

        #region Member Methods
        internal override void Deserialize(PropertyInfo property, object obj, DronNumber node)
        {
            switch (Type.GetTypeCode(property.PropertyType))
            {
                case TypeCode.Byte:
                    property.SetValue(obj, (byte)node.Value);
                    break;
                case TypeCode.SByte:
                    property.SetValue(obj, (sbyte)node.Value);
                    break;
                case TypeCode.UInt16:
                    property.SetValue(obj, (ushort)node.Value);
                    break;
                case TypeCode.Int16:
                    property.SetValue(obj, (short)node.Value);
                    break;
                case TypeCode.UInt32:
                    property.SetValue(obj, (uint)node.Value);
                    break;
                case TypeCode.Int32:
                    property.SetValue(obj, (int)node.Value);
                    break;
                case TypeCode.UInt64:
                    property.SetValue(obj, (ulong)node.Value);
                    break;
                case TypeCode.Int64:
                    property.SetValue(obj, (long)node.Value);
                    break;
                case TypeCode.Single:
                    property.SetValue(obj, (float)node.Value);
                    break;
                case TypeCode.Double:
                    property.SetValue(obj, (double)node.Value);
                    break;
                case TypeCode.Decimal:
                    property.SetValue(obj, (decimal)node.Value);
                    break;
                default:
                    throw new Exception($"Unsupported numeric type '{property.PropertyType.Name}");
            }
        }
        #endregion

        #endregion
    }
}