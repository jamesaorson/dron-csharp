using DRON.Exceptions;

namespace DRON.Lex.Exceptions
{
    public class DronUnexpectedCharacterInNumberLiteralException : DronException
    {        
        public DronUnexpectedCharacterInNumberLiteralException(char character)
            : base($"Unexpected character in number literal '{character}'") {}
    }
}