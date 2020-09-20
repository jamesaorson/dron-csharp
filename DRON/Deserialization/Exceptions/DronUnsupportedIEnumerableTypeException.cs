using System;

namespace DRON.Deserialization.Exceptions
{
    public class DronUnsupportedIEnumerableTypeException : Exception
    {        
        public DronUnsupportedIEnumerableTypeException(Type unsupportedType)
            : base($"Unsupported IEnumerable type '{unsupportedType.Name}'") {}
    }
}