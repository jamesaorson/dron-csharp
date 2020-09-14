namespace DRON.Parse
{
    public class DronAttribute : DronNode
    {
        #region Public

        #region Constructors
        public DronAttribute(string name)
            : this(name, null) {}

        public DronAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }
        #endregion

        #region Members
        public readonly string Name;
        public readonly string Value;
        #endregion

        #endregion
    }
}
