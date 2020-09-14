using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class StringDeserializer : DeserializerBase<DronString>
    {
        #region Internal

        #region Static Methods
        internal override void Deserialize(PropertyInfo property, object obj, DronString node)
        {
            if (property.PropertyType == typeof(Guid))
            {
                property.SetValue(obj, new Guid(node.Value));
                return;
            }
            property.SetValue(obj, node.Value);
        }
        #endregion

        #endregion
    }
}