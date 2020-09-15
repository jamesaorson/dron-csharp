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
            if (name[0] == '"')
            {
                name = name.Substring(1);
                if (name[name.Length - 1] == '"')
                {
                    name = name.Substring(0, name.Length - 1);
                }
            }
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