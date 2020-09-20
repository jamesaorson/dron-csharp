using System;
using DRON.Exceptions;

namespace DRON.Serialization.Exceptions
{
    public class DronUnsupportedDictionaryKeyType : DronException
    {        
        public DronUnsupportedDictionaryKeyType(Type unsupportedKeyType)
            : base($"Dictionary key was unsupported type '{unsupportedKeyType.Name}'") {}
    }
}