using System;
using System.Text;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class FloatingNumberSerializer : SerializerBase<DronFloatingNumber, object>
    {
        #region Internal

        #region Constructors
        internal FloatingNumberSerializer(Serializer serializer)
            : base(serializer) {}
        #endregion

        #region Member Methods
        internal override DronFloatingNumber Serialize(object number)
            => new DronFloatingNumber(ConvertToFloatingNumber(number));
        
        internal void ToDronSourceString(DronFloatingNumber node, StringBuilder builder)
            => builder.Append(node.Value);
        #endregion

        #region Static Methods
        internal static double ConvertToFloatingNumber(object number)
            => IsConvertible(number)
                ? Convert.ToDouble(number)
                : throw new Exception($"Unsupported floating type '{number.GetType().Name}'");
        
        internal static bool IsConvertible(object number)
            => number switch
            {
                Single or Double or Decimal => true,
                _ => false,
            };
        #endregion

        #endregion
    }
}