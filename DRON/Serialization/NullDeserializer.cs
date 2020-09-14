using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class NullDeserializer : DeserializerBase<DronNull>
    {
        #region Internal

        #region Member Methods
        internal override void Deserialize(PropertyInfo property, object obj, DronNull _)
        {
            property.SetValue(obj, null);
        }
        #endregion

        #endregion
    }
}