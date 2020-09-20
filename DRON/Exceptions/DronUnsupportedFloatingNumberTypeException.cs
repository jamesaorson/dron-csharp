using System;

namespace DRON.Exceptions
{
    public class DronUnsupportedFloatingNumberTypeException : Exception
    {        
        public DronUnsupportedFloatingNumberTypeException(Type unsupportedType)
            : base($"Unsupported floating numeric type '{unsupportedType.Name}'") {}
    }
}