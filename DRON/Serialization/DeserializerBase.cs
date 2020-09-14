using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal abstract class DeserializerBase<T>
        where T : DronNode
    {
        #region Internal

        #region Member Methods
        internal abstract void Deserialize(PropertyInfo property, object obj, T dronValue);
        #endregion

        #endregion
    }
}