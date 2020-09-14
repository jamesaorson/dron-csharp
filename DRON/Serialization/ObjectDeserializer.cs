using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ObjectDeserializer : DeserializerBase<DronObject>
    {
        #region Internal

        #region Member Methods
        internal override object Deserialize(
            DronObject dronObject,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null
        )
        {
            var propertyType = typeOverride ?? property?.PropertyType;
            if (propertyType is null)
            {
                throw new Exception("Must provide type guidance to deserialize a DronObject");
            }
            var deserializedObj = Deserializer.Deserialize(dronObject, propertyType);
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