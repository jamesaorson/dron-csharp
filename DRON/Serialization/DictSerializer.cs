using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class DictSerializer : SerializerBase<DronObject, IDictionary>
    {
        #region Internal

        #region Constructors
        internal DictSerializer(Serializer serializer)
            : base(serializer) {}
        #endregion

        #region Member Methods
        internal override DronObject Serialize(IDictionary dict)
        {
            return new DronObject(
                new List<DronAttribute>(),
                ConvertDictToFields(dict)
            );
        }
        #endregion

        #region Member Methods
        internal IReadOnlyDictionary<string, DronField> ConvertDictToFields(IDictionary dict)
        {
            if (dict is null)
            {
                return null;
            }
            var fields = new Dictionary<string, DronField>();
            foreach (var key in dict.Keys)
            {
                switch (key)
                {
                    case not String and not Guid:
                        throw new Exception($"Dictionary key was unsupported type '{key.GetType().Name}'");
                }
                var fieldName = key as string;
                var fieldNode = _serializer.Serialize(dict[key]);
                fields[fieldName] = new DronField(
                    new List<DronAttribute>(),
                    fieldName,
                    fieldNode
                );
            }
            return fields;
        }       
        internal void ToDronSourceString(DronObject node, StringBuilder builder)
            => _serializer._objectSerializer.ToDronSourceString(node, builder);
        #endregion

        #endregion
    }
}