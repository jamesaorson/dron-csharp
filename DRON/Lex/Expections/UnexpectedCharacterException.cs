using System;

namespace DRON.Lex.Exceptions
{
    public class UnexpectedCharacterException : Exception
    {        
        public UnexpectedCharacterException(char character)
            : base($"Unexpected character: '{character}'") {}
    }
}