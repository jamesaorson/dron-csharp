using DRON.Exceptions;

namespace DRON.Deserialization.Exceptions
{
    public class DronTypeGuidanceException : DronException
    {        
        public DronTypeGuidanceException()
            : base($"Must provide type guidance when deserializing") {}
    }
}