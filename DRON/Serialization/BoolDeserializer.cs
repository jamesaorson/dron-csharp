using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class BoolDeserializer : DeserializerBase<DronBool>
    {
        #region Internal

        #region Member Methods
        internal override void Deserialize(PropertyInfo property, object obj, DronBool node)
        {
            property.SetValue(obj, node.Value);
        }
        #endregion

        #endregion
    }
}