using System;

namespace DRON.Lex.Exceptions
{
    public class DronUnexpectedCharacterInNumberLiteralException : Exception
    {        
        public DronUnexpectedCharacterInNumberLiteralException(char character)
            : base($"Unexpected character in number literal '{character}'") {}
    }
}