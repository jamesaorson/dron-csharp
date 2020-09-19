using System;
using System.Collections;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal static class Serializer
    {
        static Serializer()
        {
            _boolSerializer = new BoolSerializer();
            _dictSerializer = new DictSerializer();
            _listSerializer = new ListSerializer();
            _nullSerializer = new NullSerializer();
            _floatingNumberSerializer = new FloatingNumberSerializer();
            _integralNumberSerializer = new IntegralNumberSerializer();
            _objectSerializer = new ObjectSerializer();
            _stringSerializer = new StringSerializer();
        }
        #region Internal

        #region Static Methods
        internal static DronNode Serialize<T>(T obj)
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

        internal static string ToDronSourceString(DronNode node)
        {
            var builder = new StringBuilder();
            switch (node)
            {
                case DronBool boolNode:
                    BoolSerializer.ToDronSourceString(boolNode, builder);
                    break;
                case DronObject objNode:
                    ObjectSerializer.ToDronSourceString(objNode, builder);
                    break;
                case DronFloatingNumber floatingNode:
                    FloatingNumberSerializer.ToDronSourceString(floatingNode, builder);
                    break;
                case DronIntegralNumber integralNode:
                    IntegralNumberSerializer.ToDronSourceString(integralNode, builder);
                    break;
                case DronList listNode:
                    ListSerializer.ToDronSourceString(listNode, builder);
                    break;
                case DronNull nullNode:
                    NullSerializer.ToDronSourceString(nullNode, builder);
                    break;
                case DronString stringNode:
                    StringSerializer.ToDronSourceString(stringNode, builder);
                    break;
                default:
                    throw new Exception($"Unsupported Dron node type {node.GetType().Name}");
            }
            return builder.ToString();
        }
        #endregion

        #endregion

        #region Private

        #region Static Members
        private readonly static BoolSerializer _boolSerializer;
        private readonly static DictSerializer _dictSerializer;
        private readonly static ListSerializer _listSerializer;
        private readonly static NullSerializer _nullSerializer;
        private readonly static FloatingNumberSerializer _floatingNumberSerializer;
        private readonly static IntegralNumberSerializer _integralNumberSerializer;
        private readonly static ObjectSerializer _objectSerializer;
        private readonly static StringSerializer _stringSerializer;
        #endregion

        #region Static Methods
        #endregion

        #endregion
    }
}