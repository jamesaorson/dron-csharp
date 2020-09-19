using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class NullDeserializer : DeserializerBase<DronNull>
    {
        #region Internal

        #region Constructors
        internal NullDeserializer(Deserializer deserializer)
            : base(deserializer) {}
        #endregion

        #region Member Methods
        internal override object Deserialize(
            DronNull _,
            PropertyInfo property = null,
            object obj = null,
            Type _typeOverride = null
        )
        {
            if (obj is not null)
            {
                property?.SetValue(obj, null);
            }
            return null;
        }
        #endregion

        #endregion
    }
}