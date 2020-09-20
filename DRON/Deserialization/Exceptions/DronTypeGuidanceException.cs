using System;

namespace DRON.Deserialization.Exceptions
{
    public class DronTypeGuidanceException : Exception
    {        
        public DronTypeGuidanceException()
            : base($"Must provide type guidance when deserializing") {}
    }
}