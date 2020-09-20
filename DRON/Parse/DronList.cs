using System.Collections.Generic;

namespace DRON.Parse
{
    public class DronList : DronAttributedNode
    {
        #region Public

        #region Constructors
        public DronList(
            IReadOnlyList<DronAttribute> attributes,
            IReadOnlyList<DronNode> items
        ) : base(attributes)
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
