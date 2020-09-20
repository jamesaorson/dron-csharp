using System;

namespace DRON.Lex.Exceptions
{
    public class DronUnexpectedCharacterException : Exception
    {        
        public DronUnexpectedCharacterException(char character)
            : base($"Unexpected character: '{character}'") {}
    }
}