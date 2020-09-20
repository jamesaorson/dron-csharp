using System;

namespace DRON.Exceptions
{
    public class DronUnsupportedStringTypeException : DronException
    {        
        public DronUnsupportedStringTypeException(Type unsupportedType)
            : base($"Unsupported String type '{unsupportedType.Name}'") {}
    }
}