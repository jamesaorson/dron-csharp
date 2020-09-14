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
            Name = name.Substring(1, name.Length - 2);
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