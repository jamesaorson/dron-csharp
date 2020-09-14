using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal static class Deserializer
    {
        static Deserializer()
        {
            _boolDeserializer = new BoolDeserializer();
            _dictDeserializer = new DictDeserializer();
            _listDeserializer = new ListDeserializer();
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
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                return _dictDeserializer.Deserialize(node as DronObject, typeOverride: returnType);
            }
            object deserializedObj = Activator.CreateInstance(returnType);
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
                            _numberDeserializer.Deserialize(dronNumber, property, deserializedObj);
                            break;
                        case DronString dronString:
                            _stringDeserializer.Deserialize(dronString, property, deserializedObj);
                            break;
                        case DronObject dronObject:
                            _objectDeserializer.Deserialize(dronObject, property, deserializedObj);
                            break;
                        case DronList dronList:
                            _listDeserializer.Deserialize(dronList, property, deserializedObj);
                            break;
                        case DronNull dronNull:
                            _nullDeserializer.Deserialize(dronNull, property, deserializedObj);
                            break;
                        case DronBool dronBool:
                            _boolDeserializer.Deserialize(dronBool, property, deserializedObj);
                            break;
                    }
                }
            }
            return deserializedObj;
        }

        internal static object DeserializeNode(DronNode node, object obj = null, PropertyInfo property = null, Type typeOverride = null)
            => node switch
            {
                DronNumber dronNumber => _numberDeserializer.Deserialize(dronNumber, property, obj, typeOverride),
                DronString dronString => _stringDeserializer.Deserialize(dronString, property, obj),
                DronObject dronObject => _objectDeserializer.Deserialize(dronObject, property, obj, typeOverride),
                DronList dronList => _listDeserializer.Deserialize(dronList, property, obj, typeOverride),
                DronNull dronNull => _nullDeserializer.Deserialize(dronNull, property, obj),
                DronBool dronBool => _boolDeserializer.Deserialize(dronBool, property, obj),
                _ => throw new Exception($"Unsupported DronNode '{node.GetType().Name}'"),
            };
        #endregion

        #endregion

        #region Private

        #region Static Members
        private readonly static BoolDeserializer _boolDeserializer;
        private readonly static DictDeserializer _dictDeserializer;
        private readonly static ListDeserializer _listDeserializer;
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