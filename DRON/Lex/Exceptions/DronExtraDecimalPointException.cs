using DRON.Exceptions;

namespace DRON.Lex.Exceptions
{
    public class DronExtraDecimalPointException : DronException
    {        
        public DronExtraDecimalPointException()
            : base("Extra decimal found in floating point number") {}
    }
}