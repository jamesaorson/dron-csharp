using System;

namespace DRON.Exceptions
{
    public class DronUnsupportedFloatingNumberTypeException : DronException
    {
        public DronUnsupportedFloatingNumberTypeException(Type unsupportedType)
            : base($"Unsupported floating numeric type '{unsupportedType.Name}'") {}
    }
}