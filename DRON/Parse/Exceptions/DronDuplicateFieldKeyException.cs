using System;

namespace DRON.Parse.Exceptions
{
    public class DronDuplicateFieldKeyException : Exception
    {
        public DronDuplicateFieldKeyException(string duplicateKey)
            : base($"Duplicate object key: {duplicateKey}") {}
    }
}