using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRON.Deserialization.Exceptions;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class ObjectDeserializer : DeserializerBase<DronObject>
    {
        #region Internal

        #region Constructors
        internal ObjectDeserializer(Deserializer deserializer)
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
            var propertyType = typeOverride ?? property?.PropertyType;
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
            var deserializedObj = _deserializer.Deserialize(dronObject, propertyType);
            if (obj is not null)
            {
                property?.SetValue(obj, deserializedObj);
            }
            return deserializedObj;
        }
        #endregion

        #endregion
    }
}