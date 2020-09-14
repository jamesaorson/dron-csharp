using System.Collections.Generic;

namespace DRON.Parse
{
    public abstract class DronAttributedNode : DronNode
    {
        #region Protected

        #region Constructors
        protected DronAttributedNode(IReadOnlyList<DronAttribute> attributes)
        {
            Attributes = attributes;
        }
        #endregion

        #endregion

        #region Public

        #region Members
        public readonly IReadOnlyList<DronAttribute> Attributes;
        #endregion

        #endregion
    }
}
