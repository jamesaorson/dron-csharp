using DRON.Exceptions;

namespace DRON.Lex.Exceptions
{
    public class DronExpectedDigitAfterNegativeSignException : DronException
    {        
        public DronExpectedDigitAfterNegativeSignException()
            : base("Expected at least one digit following a negative sign") {}
    }
}