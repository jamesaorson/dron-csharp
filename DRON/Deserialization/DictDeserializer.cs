using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRON.Deserialization.Exceptions;
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
            Type typeOverride = null,
            IReadOnlyList<DronAttribute> additionalAttributes = null
        )
        {
            var propertyType = typeOverride is not null
                ? typeOverride : property?.PropertyType;
            IEnumerable<DronAttribute> allAttributes = dronObject.Attributes;
            if (additionalAttributes is not null)
            {
                allAttributes = allAttributes.Concat(additionalAttributes);
            }
            var typeAttribute = allAttributes.FirstOrDefault(
                attribute => attribute.Name == DronAttribute.TYPE
            );
            if (typeAttribute is not null)
            {
                propertyType = Type.GetType(typeAttribute.Value);
            }
            if (propertyType is null)
            {
                throw new DronTypeGuidanceException();
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