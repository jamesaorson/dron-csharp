using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal static class Deserializer
    {
        static Deserializer()
        {
            _boolDeserializer = new BoolDeserializer();
            _nullDeserializer = new NullDeserializer();
            _numberDeserializer = new NumberDeserializer();
            _objectDeserializer = new ObjectDeserializer();
            _stringDeserializer = new StringDeserializer();
        }
        #region Internal

        #region Static Methods
        internal static T Deserialize<T>(DronNode node)
            where T : new()
                => (T)Deserialize(node, typeof(T));

        internal static object Deserialize(DronNode node, Type returnType)
        {
            var deserializedObj = Activator.CreateInstance(returnType);
            if (node is DronObject obj)
            {
                foreach (var pair in obj.Fields)
                {
                    var field = pair.Value;
                    var properties = returnType.GetProperties();
                    var property = GetPropertyInfoByField(field, returnType);
                    if (property is null)
                    {
                        throw new Exception($"Property {pair.Key} does not exist on type '{returnType.Name}'");
                    }
                    switch (field.Value)
                    {
                        case DronNumber dronNumber:
                            _numberDeserializer.Deserialize(property, deserializedObj, dronNumber);
                            break;
                        case DronString dronString:
                            _stringDeserializer.Deserialize(property, deserializedObj, dronString);
                            break;
                        case DronObject dronObject:
                            _objectDeserializer.Deserialize(property, deserializedObj, dronObject);
                            break;
                        case DronNull dronNull:
                            _nullDeserializer.Deserialize(property, deserializedObj, dronNull);
                            break;
                        case DronBool dronBool:
                            _boolDeserializer.Deserialize(property, deserializedObj, dronBool);
                            break;
                    }
                }
            }
            return deserializedObj;
        }
        #endregion

        #endregion

        #region Private

        #region Static Members
        private readonly static BoolDeserializer _boolDeserializer;
        private readonly static NullDeserializer _nullDeserializer;
        private readonly static NumberDeserializer _numberDeserializer;
        private readonly static ObjectDeserializer _objectDeserializer;
        private readonly static StringDeserializer _stringDeserializer;
        #endregion

        #region Static Methods
        private static PropertyInfo GetPropertyInfoByField<T>(DronField field)
            where T : new()
                => GetPropertyInfoByField(field, typeof(T));
            
        private static PropertyInfo GetPropertyInfoByField(DronField field, Type fieldType)
            => fieldType.GetProperty(field.Name);
        #endregion

        #endregion
    }
}