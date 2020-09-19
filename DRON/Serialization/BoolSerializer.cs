using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class BoolSerializer : SerializerBase<DronBool, bool>
    {
        #region Internal

        #region Member Methods
        internal override DronBool Serialize(bool value)
            => value ? new DronTrue() : new DronFalse();

        #endregion
        
        #region Static Methods
        internal static void ToDronSourceString(DronBool node, StringBuilder builder)
            => builder.Append(node.Value.ToString().ToLower());
        #endregion

        #endregion
    }
}