using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class BoolDeserializer : DeserializerBase<DronBool>
    {
        #region Internal

        #region Member Methods
        internal override object Deserialize(
            DronBool node,
            PropertyInfo property = null,
            object obj = null,
            Type _typeOverride = null
        )
        {
            if (obj is not null)
            {
                property?.SetValue(obj, node.Value);
            }
            return node.Value;
        }
        #endregion

        #endregion
    }
}