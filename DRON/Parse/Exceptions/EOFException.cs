using System;
using DRON.Tokens;

namespace DRON.Parse.Exceptions
{
    public class EOFException : Exception
    {
        public EOFException(Type dronType)
            : base($"Unexpected EOF while parsing {dronType.Name}") {}
    }
}