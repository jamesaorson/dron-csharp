using System;

namespace DRON.Parse.Exceptions
{
    public class DronUnexpectedEOFException : Exception
    {
        public DronUnexpectedEOFException(Type dronType)
            : base($"Unexpected EOF while parsing {dronType.Name}") {}
    }
}