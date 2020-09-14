namespace DRON.Parse
{
    public class DronObjectRef : DronNode
    {
        #region Public

        #region Constructors
        public DronObjectRef(string value)
        {
            Value = value;
        }
        #endregion

        #region Members
        public readonly string Value;
        #endregion

        #endregion
    }
}