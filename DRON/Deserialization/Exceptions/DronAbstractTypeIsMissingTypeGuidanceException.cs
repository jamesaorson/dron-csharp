using System;
using DRON.Exceptions;

namespace DRON.Deserialization.Exceptions
{
    public class DronAbstractTypeIsMissingTypeGuidanceException : DronException
    {        
        public DronAbstractTypeIsMissingTypeGuidanceException(Type type)
            : base($"Cannot deserialize abstract type '{type}' without concrete type guidance from a DRON attribute") {}
    }
}