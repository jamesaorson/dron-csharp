using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class DictDeserializer : DeserializerBase<DronObject>
    {
        #region Internal

        #region Constructors
        internal DictDeserializer(Deserializer deserializer)
            : base(deserializer) {}
        #endregion

        #region Member Methods
        internal override object Deserialize(
            DronObject dronObject,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null
        )
        {
            var propertyType = typeOverride is not null
                ? typeOverride : property?.PropertyType;
            if (propertyType is null)
            {
                throw new Exception("Must provide type guidance to deserialize a dictionary");
            }
            var typeArguments = propertyType.GetGenericArguments();
            var keyType = typeArguments[0];
            var valueType = typeArguments[1];
            var dictType = typeof(Dictionary<,>).MakeGenericType(
                keyType,
                valueType
            );
            var dict = Activator.CreateInstance(dictType) as IDictionary;
            foreach (var field in dronObject.Fields)
            {
                var key = StringDeserializer.ConvertString(field.Key, keyType);
                dict[key] = _deserializer.DeserializeNode(
                    field.Value.Value,
                    typeOverride: valueType
                );
            }
            return dict;
        }
        #endregion

        #endregion
    }
}