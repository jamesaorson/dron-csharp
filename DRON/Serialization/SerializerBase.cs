using DRON.Parse;

namespace DRON.Serialization
{
    internal abstract class SerializerBase<TDron, T>
        where TDron : DronNode
    {
        #region Internal

        #region Member Methods
        internal abstract TDron Serialize(T value);
        #endregion

        #endregion
    }
}