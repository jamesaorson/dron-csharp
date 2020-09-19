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

        #region Constructors
        internal ListSerializer(Serializer serializer)
            : base(serializer) {}
        #endregion

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
                items.Add(_serializer.Serialize(item));
            }
            return new DronList(items);
        }

        internal void ToDronSourceString(DronList node, StringBuilder builder)
        {
            builder.Append(LIST_START);
            builder.AppendJoin(
                ',',
                node.Items.Select(item => _serializer.ToDronSourceString(item))
            );
            builder.Append(LIST_END);
        }
        #endregion

        #endregion
    }
}