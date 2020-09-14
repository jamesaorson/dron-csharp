namespace DRON.Parse
{
    public abstract class DronBool : DronNode
    {
        #region Public

        #region Members
        public readonly bool Value;
        #endregion

        #endregion

        #region Protected

        #region Constructors
        protected DronBool(bool value)
        {
            Value = value;
        }
        #endregion

        #endregion
    }
}
