using System;
using DRON.Parse;

namespace DRON.Deserialization.Exceptions
{
    public class DronPropertyDoesNotExistException : Exception
    {        
        public DronPropertyDoesNotExistException(DronField field, Type type)
            : base($"Property {field.Name} does not exist on type '{type.Name}'") {}
    }
}