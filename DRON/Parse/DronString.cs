namespace DRON.Parse
{
    public class DronString : DronNode
    {
        #region Public

        #region Constructors
        public DronString(string value)
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