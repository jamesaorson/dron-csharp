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
            Name = TrimQuotes(name);
            Value = TrimQuotes(value);
        }
        #endregion

        #region Constants
        public const string ITEM_TYPE = "ItemType";
        public const string TYPE = "Type";
        #endregion

        #region Members
        public readonly string Name;
        public readonly string Value;
        #endregion

        #endregion
    }
}
