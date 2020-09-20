using System;
using DRON.Exceptions;

namespace DRON.Deserialization.Exceptions
{
    public class DronUnsupportedIEnumerableTypeException : DronException
    {        
        public DronUnsupportedIEnumerableTypeException(Type unsupportedType)
            : base($"Unsupported IEnumerable type '{unsupportedType.Name}'") {}
    }
}