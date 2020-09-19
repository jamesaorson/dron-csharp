using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class BoolSerializer : SerializerBase<DronBool, bool>
    {
        #region Internal

        #region Constructors
        internal BoolSerializer(Serializer serializer)
            : base(serializer) {}
        #endregion

        #region Member Methods
        internal override DronBool Serialize(bool value)
            => value ? new DronTrue() : new DronFalse();

        internal void ToDronSourceString(DronBool node, StringBuilder builder)
            => builder.Append(node.Value.ToString().ToLower());
        #endregion

        #endregion
    }
}