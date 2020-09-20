using System.Collections.Generic;

namespace DRON.Parse
{
    public class DronField : DronAttributedNode
    {
        #region Public

        #region Constructors
        public DronField(
            IReadOnlyList<DronAttribute> attributes,
            string name,
            DronNode value
        )
            : base(attributes)
        {
            name = TrimQuotes(name);
            Name = name;
            Value = value;
        }
        #endregion

        #region Members
        public readonly string Name;
        public readonly DronNode Value;
        #endregion

        #endregion
    }
}