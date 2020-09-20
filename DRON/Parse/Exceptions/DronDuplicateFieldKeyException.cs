using DRON.Exceptions;

namespace DRON.Parse.Exceptions
{
    public class DronDuplicateFieldKeyException : DronException
    {
        public DronDuplicateFieldKeyException(string duplicateKey)
            : base($"Duplicate object key: {duplicateKey}") {}
    }
}