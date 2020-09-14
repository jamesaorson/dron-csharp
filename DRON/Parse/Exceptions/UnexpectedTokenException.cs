using System;
using DRON.Tokens;

namespace DRON.Parse.Exceptions
{
    internal class UnexpectedTokenException : Exception
    {
        internal UnexpectedTokenException(Type dronType, Token badToken)
            : base($"Unexpected token while parsing {dronType.Name}: {badToken.Kind}") {}
    }
}