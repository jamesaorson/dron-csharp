using System;
using DRON.Exceptions;
using DRON.Tokens;

namespace DRON.Parse.Exceptions
{
    internal class DronUnexpectedTokenException : DronException
    {
        internal DronUnexpectedTokenException(Type dronType, Token badToken)
            : base($"Unexpected token while parsing {dronType.Name}: {badToken.Kind}") {}
    }
}