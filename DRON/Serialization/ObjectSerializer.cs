using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ObjectSerializer : SerializerBase<DronObject, object>
    {
        #region Internal

        #region Constructors
        internal ObjectSerializer(Serializer serializer)
            : base(serializer) {}
        #endregion

        #region Constants
        internal const char OBJECT_START = '{';
        internal const char OBJECT_END = '}';
        #endregion

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
                _serializer._dictSerializer.ConvertDictToFields(fieldsDict)
            );
        }

        internal void ToDronSourceString(DronObject node, StringBuilder builder)
        {
            builder.Append(OBJECT_START);
            builder.AppendJoin(
                ',',
                node.Fields.Select(
                    pair => ToDronSourceString(pair.Key, pair.Value)
                )
            );
            builder.Append(OBJECT_END);
        }
        #endregion

        #endregion

        #region Private

        #region Member Methods
        private string ToDronSourceString(string fieldName, DronField field)
            => $"\"{fieldName}\": {_serializer.ToDronSourceString(field.Value)}";
        #endregion

        #endregion
    }
}