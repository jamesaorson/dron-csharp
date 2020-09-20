using DRON.Exceptions;

namespace DRON.Lex.Exceptions
{
    public class DronExpectedDigitAfterDecimalPointException : DronException
    {        
        public DronExpectedDigitAfterDecimalPointException()
            : base("Expected at least one digit following a decimal point") {}
    }
}