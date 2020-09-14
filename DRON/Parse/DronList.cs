using System.Collections.Generic;

namespace DRON.Parse
{
    public class DronList : DronNode
    {
        #region Public

        #region Constructors
        public DronList(IReadOnlyList<DronNode> items)
        {
            Items = items;
        }
        #endregion

        #region Members
        public readonly IReadOnlyList<DronNode> Items;
        #endregion

        #endregion
    }
}
