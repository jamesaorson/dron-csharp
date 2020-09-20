using DRON.Exceptions;

namespace DRON.Lex.Exceptions
{
    public class DronUnexpectedCharacterException : DronException
    {        
        public DronUnexpectedCharacterException(char character)
            : base($"Unexpected character: '{character}'") {}
    }
}