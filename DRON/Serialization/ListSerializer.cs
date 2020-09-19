using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ListSerializer : SerializerBase<DronList, IEnumerable>
    {
        #region Internal

        #region Constants
        internal const char LIST_START = '[';
        internal const char LIST_END = ']';
        #endregion

        #region Member Methods
        internal override DronList Serialize(IEnumerable list)
        {
            if (list is null)
            {
                return null;
            }
            var items = new List<DronNode>();
            foreach (var item in list)
            {
                items.Add(Serializer.Serialize(item));
            }
            return new DronList(items);
        }
        #endregion

        #region Static Methods
        internal static void ToDronSourceString(DronList node, StringBuilder builder)
        {
            builder.Append(LIST_START);
            builder.AppendJoin(
                ',',
                node.Items.Select(item => Serializer.ToDronSourceString(item))
            );
            builder.Append(LIST_END);
        }
        #endregion

        #endregion
    }
}