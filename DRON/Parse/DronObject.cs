using System.Collections.Generic;

namespace DRON.Parse
{
    public class DronObject : DronAttributedNode
    {
        #region Public

        #region Constructors
        public DronObject(
            IReadOnlyList<DronAttribute> attributes,
            IReadOnlyDictionary<string, DronField> fields
        )
            : base(attributes)
        {
            Fields = fields;   
        }
        #endregion

        #region Members
        public readonly IReadOnlyDictionary<string, DronField> Fields;
        #endregion

        #endregion
    }
}
