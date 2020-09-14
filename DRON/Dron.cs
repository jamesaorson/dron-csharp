using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DRON.Lex;
using DRON.Parse;

namespace DRON
{
    public static class Dron
    {
        #region Public

        #region Static Methods
        public static T Parse<T>([NotNull] string dronString)
            where T : new()
            => ParseAsync<T>(dronString).Result;
        
        public async static Task<T> ParseAsync<T>([NotNull] string dronString)
            where T : new()
            => await ParseAsync<T>(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(dronString ?? "")
                )
            );

        public static T Parse<T>([NotNull] Stream stream)
            where T : new()
            => ParseAsync<T>(stream).Result;
        
        public async static Task<T> ParseAsync<T>([NotNull] Stream stream)
            where T : new()
        {
            var node = await Task.Run(() =>
                {
                    var lexer = new Lexer();
                    var parser = new Parser();
                    return parser.Parse(lexer.Lex(stream));
                }
            );
            return Deserialize<T>(node);
        }
        #endregion

        #endregion

        #region Private

        #region Static Methods
        private static T Deserialize<T>(DronNode node)
            where T : new()
        {
            var deserialized = new T();
            if (node is DronObject obj)
            {
                foreach (var pair in obj.Fields)
                {
                    var field = pair.Value;
                    var properties = typeof(T).GetProperties();
                    var property = GetPropertyInfoByField<T>(field);
                    if (property is null)
                    {
                        throw new Exception($"Property {pair.Key} does not exist on type '{typeof(T).Name}'");
                    }
                    switch (field.Value)
                    {
                        case DronNumber dronNumber:
                            switch (Type.GetTypeCode(property.PropertyType))
                            {
                                case TypeCode.Byte:
                                    property.SetValue(deserialized, (byte)dronNumber.Value);
                                    break;
                                case TypeCode.SByte:
                                    property.SetValue(deserialized, (sbyte)dronNumber.Value);
                                    break;
                                case TypeCode.UInt16:
                                    property.SetValue(deserialized, (ushort)dronNumber.Value);
                                    break;
                                case TypeCode.Int16:
                                    property.SetValue(deserialized, (short)dronNumber.Value);
                                    break;
                                case TypeCode.UInt32:
                                    property.SetValue(deserialized, (uint)dronNumber.Value);
                                    break;
                                case TypeCode.Int32:
                                    property.SetValue(deserialized, (int)dronNumber.Value);
                                    break;
                                case TypeCode.UInt64:
                                    property.SetValue(deserialized, (ulong)dronNumber.Value);
                                    break;
                                case TypeCode.Int64:
                                    property.SetValue(deserialized, (long)dronNumber.Value);
                                    break;
                                case TypeCode.Single:
                                    property.SetValue(deserialized, (float)dronNumber.Value);
                                    break;
                                case TypeCode.Double:
                                    property.SetValue(deserialized, (double)dronNumber.Value);
                                    break;
                                case TypeCode.Decimal:
                                    property.SetValue(deserialized, (decimal)dronNumber.Value);
                                    break;
                                default:
                                    throw new Exception($"Unsupported numeric type '{property.PropertyType.Name}");
                            }
                            break;
                        case DronString dronString:
                            if (property.PropertyType == typeof(Guid))
                            {
                                property.SetValue(deserialized, new Guid(dronString.Value));
                                break;
                            }
                            property.SetValue(deserialized, dronString.Value);
                            break;
                        case DronNull:
                            property.SetValue(deserialized, null);
                            break;
                        case DronTrue:
                            property.SetValue(deserialized, true);
                            break;
                        case DronFalse:
                            property.SetValue(deserialized, false);
                            break;
                    }
                }
            }
            return deserialized;
        }

        private static PropertyInfo GetPropertyInfoByField<T>(DronField field)
            where T : new()
                => typeof(T).GetProperty(field.Name);
        #endregion

        #endregion
    }
}