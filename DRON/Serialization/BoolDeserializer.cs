using System.Reflection;

namespace DRON.Serialization
{
    internal class BoolDeserializer : DeserializerBase<bool>
    {
        #region Internal

        #region Member Methods
        internal override void Deserialize(PropertyInfo property, object obj, bool value)
        {
            property.SetValue(obj, value);
        }
        #endregion

        #endregion
    }
}