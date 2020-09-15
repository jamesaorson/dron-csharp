using System.Collections.Generic;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ObjectSerializer : SerializerBase<DronObject, object>
    {
        #region Internal

        #region Member Methods
        internal override DronObject Serialize(object value)
        {
            var fieldsDict = new Dictionary<string, object>();
            var properties = value.GetType().GetProperties();
            foreach (var property in properties)
            {
                fieldsDict[property.Name] = property.GetValue(value);
            }
            return new DronObject(
                new List<DronAttribute>(),
                DictSerializer.ConvertDictToFields(fieldsDict)
            );
        }
        #endregion

        #endregion
    }
}