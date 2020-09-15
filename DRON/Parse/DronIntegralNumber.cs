namespace DRON.Parse
{
    public class DronIntegralNumber : DronNode
    {
        #region Public

        #region Constructors
        public DronIntegralNumber(long value)
        {
            Value = value;
        }
        #endregion

        #region Members
        public readonly long Value;
        #endregion

        #endregion
    }
}