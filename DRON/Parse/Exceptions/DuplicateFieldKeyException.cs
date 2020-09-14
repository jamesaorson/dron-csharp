using System;
using DRON.Tokens;

namespace DRON.Parse.Exceptions
{
    public class DuplicateFieldKeyException : Exception
    {
        public DuplicateFieldKeyException(string duplicateKey)
            : base($"Duplicate object key: {duplicateKey}") {}
    }
}