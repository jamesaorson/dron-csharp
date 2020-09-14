using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ObjectDeserializer : DeserializerBase<DronObject>
    {
        #region Internal

        #region Member Methods
        internal override void Deserialize(PropertyInfo property, object obj, DronObject dronObject)
        {
            var deserializedObj = Deserializer.Deserialize(dronObject, property.PropertyType);
            property.SetValue(deserializedObj, deserializedObj);
        }
        #endregion

        #endregion
    }
}