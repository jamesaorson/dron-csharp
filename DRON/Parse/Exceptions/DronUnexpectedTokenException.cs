using System;
using DRON.Tokens;

namespace DRON.Parse.Exceptions
{
    internal class DronUnexpectedTokenException : Exception
    {
        internal DronUnexpectedTokenException(Type dronType, Token badToken)
            : base($"Unexpected token while parsing {dronType.Name}: {badToken.Kind}") {}
    }
}