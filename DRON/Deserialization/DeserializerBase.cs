using System;
using System.Reflection;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal abstract class DeserializerBase<T>
        where T : DronNode
    {
        #region Internal

        #region Member Methods
        internal abstract object Deserialize(
            T dronValue,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null
        );
        #endregion

        #endregion
    }
}