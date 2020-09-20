using System;

namespace DRON.Lex.Exceptions
{
    public class DronExtraDecimalPointException : Exception
    {        
        public DronExtraDecimalPointException()
            : base("Extra decimal found in floating point number") {}
    }
}