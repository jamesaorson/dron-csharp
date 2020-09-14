using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal abstract class DeserializerBase<TValue>
    {
        #region Internal

        #region Member Methods
        internal abstract void Deserialize(PropertyInfo property, object obj, TValue dronValue);
        #endregion

        #endregion
    }
}