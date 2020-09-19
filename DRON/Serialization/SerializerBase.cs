using System.Text;
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

        #region Protected

        #region Constructors
        protected SerializerBase(Serializer serializer)
        {
            _serializer = serializer;
        }
        #endregion

        #region Members
        protected readonly Serializer _serializer;
        #endregion

        #endregion
    }
}