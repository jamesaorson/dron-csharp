using System;
using DRON.Exceptions;
using DRON.Parse;

namespace DRON.Deserialization.Exceptions
{
    public class DronPropertyDoesNotExistException : DronException
    {        
        public DronPropertyDoesNotExistException(DronField field, Type type)
            : base($"Property {field.Name} does not exist on type '{type.Name}'") {}
    }
}