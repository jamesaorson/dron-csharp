using System;

namespace DRON.Exceptions
{
    public abstract class DronException : Exception
    {
        #region Protected

        #region Constructors
        protected DronException(string message)
            : base(message) {}
        #endregion

        #endregion
    }
}