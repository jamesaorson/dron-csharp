using System;

namespace DRON.Exceptions
{
    public class DronUnsupportedStringTypeException : Exception
    {        
        public DronUnsupportedStringTypeException(Type unsupportedType)
            : base($"Unsupported String type '{unsupportedType.Name}'") {}
    }
}