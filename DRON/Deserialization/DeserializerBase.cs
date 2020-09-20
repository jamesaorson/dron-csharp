using System;
using System.Collections.Generic;
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
            Type typeOverride = null,
            IReadOnlyList<DronAttribute> additionalAttributes = null
        );
        #endregion

        #endregion

        #region Protected

        #region Constructors
        protected DeserializerBase(Deserializer deserializer)
        {
            _deserializer = deserializer;
        }
        #endregion
        
        #region Members
        protected readonly Deserializer _deserializer;
        #endregion

        #endregion
    }
}