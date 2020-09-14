using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class StringDeserializer : DeserializerBase<DronString>
    {
        #region Internal

        #region Static Methods
        internal override object Deserialize(
            DronString node,
            PropertyInfo property = null,
            object obj = null,
            Type _typeOverride = null
        )
        {
            dynamic value = node.Value;
            if (property is null)
            {
                return value;
            }
            if (property.PropertyType == typeof(Guid))
            {
                value = new Guid(value);
            }
            if (obj is not null)
            {
                property.SetValue(obj, value);
            }
            return value;
        }
        #endregion

        #endregion
    }
}