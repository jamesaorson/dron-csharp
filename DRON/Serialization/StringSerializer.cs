using System;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class StringSerializer : SerializerBase<DronString, object>
    {
        #region Internal

        #region Static Methods
        internal override DronString Serialize(object value)
            => new DronString(ConvertToString(value));
        #endregion

        #region Static Methods
        internal static string ConvertToString(object value)
            => value switch
            {
                String s => s,
                Guid guid => guid.ToString(),
                _ => throw new Exception($"Unsupported string type '{value.GetType().Name}'"),
            };
        
        internal static void ToDronSourceString(DronString node, StringBuilder builder)
            => builder.Append($"\"{node.Value}\"");
        #endregion

        #endregion
    }
}