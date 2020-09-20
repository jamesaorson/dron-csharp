using System;

namespace DRON.Lex.Exceptions
{
    public class DronExpectedDigitAfterDecimalPointException : Exception
    {        
        public DronExpectedDigitAfterDecimalPointException()
            : base("Expected at least one digit following a decimal point") {}
    }
}