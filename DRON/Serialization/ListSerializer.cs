using System.Collections;
using System.Collections.Generic;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ListSerializer : SerializerBase<DronList, IEnumerable>
    {
        #region Internal

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

        #endregion
    }
}