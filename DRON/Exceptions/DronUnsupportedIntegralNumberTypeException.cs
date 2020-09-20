using System;

namespace DRON.Exceptions
{
    public class DronUnsupportedIntegralNumberTypeException : DronException
    {        
        public DronUnsupportedIntegralNumberTypeException(Type unsupportedType)
            : base($"Unsupported integral numeric type '{unsupportedType.Name}'") {}
    }
}