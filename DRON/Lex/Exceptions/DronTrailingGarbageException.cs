using System;

namespace DRON.Lex.Exceptions
{
    public class DronTrailingGarbageException : Exception
    {        
        public DronTrailingGarbageException(string garbage)
            : base($"Trailing garbage characters: {garbage}") {}
    }
}