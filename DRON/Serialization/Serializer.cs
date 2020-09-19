using System;
using System.Collections;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class Serializer
    {
        internal Serializer()
        {
            _boolSerializer = new BoolSerializer(this);
            _dictSerializer = new DictSerializer(this);
            _listSerializer = new ListSerializer(this);
            _nullSerializer = new NullSerializer(this);
            _floatingNumberSerializer = new FloatingNumberSerializer(this);
            _integralNumberSerializer = new IntegralNumberSerializer(this);
            _objectSerializer = new ObjectSerializer(this);
            _stringSerializer = new StringSerializer(this);
        }
        #region Internal

        #region Member Methods
        internal DronNode Serialize<T>(T obj)
            where T : class, new()
        {
            if (FloatingNumberSerializer.IsConvertible(obj))
            {
                return _floatingNumberSerializer.Serialize(obj);
            }
            if (IntegralNumberSerializer.IsConvertible(obj))
            {
                return _integralNumberSerializer.Serialize(obj);
            }
            return obj switch
            {
                Guid or String => _stringSerializer.Serialize(obj),
                Boolean value => _boolSerializer.Serialize(value),
                IDictionary dict => _dictSerializer.Serialize(dict),
                IEnumerable list => _listSerializer.Serialize(list),
                object => _objectSerializer.Serialize(obj),
                null => _nullSerializer.Serialize(obj),
            };
        }

        internal string ToDronSourceString(DronNode node)
        {
            var builder = new StringBuilder();
            switch (node)
            {
                case DronBool boolNode:
                    _boolSerializer.ToDronSourceString(boolNode, builder);
                    break;
                case DronObject objNode:
                    _objectSerializer.ToDronSourceString(objNode, builder);
                    break;
                case DronFloatingNumber floatingNode:
                    _floatingNumberSerializer.ToDronSourceString(floatingNode, builder);
                    break;
                case DronIntegralNumber integralNode:
                    _integralNumberSerializer.ToDronSourceString(integralNode, builder);
                    break;
                case DronList listNode:
                    _listSerializer.ToDronSourceString(listNode, builder);
                    break;
                case DronNull nullNode:
                    _nullSerializer.ToDronSourceString(nullNode, builder);
                    break;
                case DronString stringNode:
                    _stringSerializer.ToDronSourceString(stringNode, builder);
                    break;
                default:
                    throw new Exception($"Unsupported Dron node type {node.GetType().Name}");
            }
            return builder.ToString();
        }
        #endregion

        #region Members
        internal readonly BoolSerializer _boolSerializer;
        internal readonly DictSerializer _dictSerializer;
        internal readonly ListSerializer _listSerializer;
        internal readonly NullSerializer _nullSerializer;
        internal readonly FloatingNumberSerializer _floatingNumberSerializer;
        internal readonly IntegralNumberSerializer _integralNumberSerializer;
        internal readonly ObjectSerializer _objectSerializer;
        internal readonly StringSerializer _stringSerializer;
        #endregion

        #endregion
    }
}