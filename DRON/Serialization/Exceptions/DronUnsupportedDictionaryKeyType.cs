using System;

namespace DRON.Serialization.Exceptions
{
    public class DronUnsupportedDictionaryKeyType : Exception
    {        
        public DronUnsupportedDictionaryKeyType(Type unsupportedKeyType)
            : base($"Dictionary key was unsupported type '{unsupportedKeyType.Name}'") {}
    }
}