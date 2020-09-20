using System;
using System.Collections;
using System.Reflection;
using DRON.Deserialization.Exceptions;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class Deserializer
    {
        internal Deserializer()
        {
            _boolDeserializer = new BoolDeserializer(this);
            _dictDeserializer = new DictDeserializer(this);
            _floatingNumberDeserializer = new FloatingNumberDeserializer(this);
            _integralNumberDeserializer = new IntegralNumberDeserializer(this);
            _listDeserializer = new ListDeserializer(this);
            _nullDeserializer = new NullDeserializer(this);
            _objectDeserializer = new ObjectDeserializer(this);
            _stringDeserializer = new StringDeserializer(this);
        }
        #region Internal

        #region Member Methods
        internal T Deserialize<T>(DronNode node)
            where T : class, new()
                => (T)Deserialize(node, typeof(T));

        internal object Deserialize(DronNode node, Type returnType)
        {
            if (returnType.GetInterface(nameof(IDictionary)) is not null)
            {
                return _dictDeserializer.Deserialize(node as DronObject, typeOverride: returnType);
            }
            if (returnType.IsAbstract)
            {
                throw new DronAbstractTypeIsMissingTypeGuidanceException(returnType);
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
                        throw new DronPropertyDoesNotExistException(pair.Value, returnType);
                    }
                    switch (field.Value)
                    {
                        case DronFloatingNumber dronNumber:
                            if (FloatingNumberDeserializer.IsConvertible(property))
                            {
                                _floatingNumberDeserializer.Deserialize(dronNumber, property, deserializedObj);
                            }
                            else
                            {
                                _integralNumberDeserializer.Deserialize(dronNumber, property, deserializedObj);    
                            }
                            break;
                        case DronIntegralNumber dronNumber:
                            if (IntegralNumberDeserializer.IsConvertible(property))
                            {
                                _integralNumberDeserializer.Deserialize(dronNumber, property, deserializedObj);    
                            }
                            else
                            {
                                var code = Type.GetTypeCode(property.GetType());
                                _floatingNumberDeserializer.Deserialize(dronNumber, property, deserializedObj);
                            }
                            break;
                        case DronString dronString:
                            _stringDeserializer.Deserialize(dronString, property, deserializedObj);
                            break;
                        case DronObject dronObject:
                            _objectDeserializer.Deserialize(dronObject, property, deserializedObj, additionalAttributes: field.Attributes);
                            break;
                        case DronList dronList:
                            _listDeserializer.Deserialize(dronList, property, deserializedObj, additionalAttributes: field.Attributes);
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
        
        internal object DeserializeNode(
            DronNode node,
            object obj = null,
            PropertyInfo property = null,
            Type typeOverride = null
        )
            => node switch
            {
                DronFloatingNumber dronNumber => _floatingNumberDeserializer.Deserialize(dronNumber, property, obj, typeOverride),
                DronIntegralNumber dronNumber => _integralNumberDeserializer.Deserialize(dronNumber, property, obj, typeOverride),
                DronString dronString => _stringDeserializer.Deserialize(dronString, property, obj),
                DronObject dronObject => _objectDeserializer.Deserialize(dronObject, property, obj, typeOverride),
                DronList dronList => _listDeserializer.Deserialize(dronList, property, obj, typeOverride),
                DronNull dronNull => _nullDeserializer.Deserialize(dronNull, property, obj),
                DronBool dronBool => _boolDeserializer.Deserialize(dronBool, property, obj),
                _ => throw new ArgumentOutOfRangeException($"Unsupported DronNode '{node.GetType().Name}'"),
            };
        #endregion

        #endregion

        #region Private

        #region Members
        private readonly BoolDeserializer _boolDeserializer;
        private readonly DictDeserializer _dictDeserializer;
        private readonly FloatingNumberDeserializer _floatingNumberDeserializer;
        private readonly IntegralNumberDeserializer _integralNumberDeserializer;
        private readonly ListDeserializer _listDeserializer;
        private readonly NullDeserializer _nullDeserializer;
        private readonly ObjectDeserializer _objectDeserializer;
        private readonly StringDeserializer _stringDeserializer;
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