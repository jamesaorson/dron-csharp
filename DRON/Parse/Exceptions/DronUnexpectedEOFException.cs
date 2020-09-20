using System;
using DRON.Exceptions;

namespace DRON.Parse.Exceptions
{
    public class DronUnexpectedEOFException : DronException
    {
        public DronUnexpectedEOFException(Type dronType)
            : base($"Unexpected EOF while parsing {dronType.Name}") {}
    }
}