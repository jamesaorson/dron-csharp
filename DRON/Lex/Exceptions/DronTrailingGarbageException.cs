using DRON.Exceptions;

namespace DRON.Lex.Exceptions
{
    public class DronTrailingGarbageException : DronException
    {        
        public DronTrailingGarbageException(string garbage)
            : base($"Trailing garbage characters: {garbage}") {}
    }
}