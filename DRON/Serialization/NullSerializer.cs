using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class NullSerializer : SerializerBase<DronNull, object>
    {
        #region Internal

        #region Constructors
        internal NullSerializer(Serializer serializer)
            : base(serializer) {}
        #endregion

        #region Member Methods
        internal override DronNull Serialize(object _) => new DronNull();
        
        internal void ToDronSourceString(DronNull _, StringBuilder builder)
            => builder.Append("null");
        #endregion

        #endregion
    }
}