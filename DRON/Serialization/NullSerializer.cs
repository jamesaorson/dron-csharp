using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class NullSerializer : SerializerBase<DronNull, object>
    {
        #region Internal

        #region Member Methods
        internal override DronNull Serialize(object _) => new DronNull();
        #endregion

        #region Static Methods
        internal static void ToDronSourceString(DronNull _, StringBuilder builder)
            => builder.Append("null");
        #endregion

        #endregion
    }
}