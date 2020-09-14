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
            Type _typeOverride = null
        )
        {
            var deserializedObj = Deserializer.Deserialize(dronObject, property.PropertyType);
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