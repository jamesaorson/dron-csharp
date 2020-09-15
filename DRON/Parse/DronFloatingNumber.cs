namespace DRON.Parse
{
    public class DronFloatingNumber : DronNode
    {
        #region Public

        #region Constructors
        public DronFloatingNumber(double value)
        {
            Value = value;
        }
        #endregion

        #region Members
        public readonly double Value;
        #endregion

        #endregion
    }
}