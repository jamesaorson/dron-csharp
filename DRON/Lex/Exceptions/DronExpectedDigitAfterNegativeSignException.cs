using System;

namespace DRON.Lex.Exceptions
{
    public class DronExpectedDigitAfterNegativeSignException : Exception
    {        
        public DronExpectedDigitAfterNegativeSignException()
            : base("Expected at least one digit following a negative sign") {}
    }
}