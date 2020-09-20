using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRON.Deserialization.Exceptions;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class ListDeserializer : DeserializerBase<DronList>
    {
        #region Internal

        #region Constructors
        internal ListDeserializer(Deserializer deserializer)
            : base(deserializer) {}
        #endregion

        #region Member Methods
        internal override object Deserialize(
            DronList dronList,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null,
            IReadOnlyList<DronAttribute> additionalAttributes = null
        )
        {
            var propertyType = typeOverride ?? property.PropertyType;
            IEnumerable<DronAttribute> allAttributes = dronList.Attributes;
            if (additionalAttributes is not null)
            {
                allAttributes = allAttributes.Concat(additionalAttributes);
            }
            var itemTypeAttribute = allAttributes.FirstOrDefault(
                attribute => attribute.Name == DronAttribute.ITEM_TYPE
            );
            Type itemType = null;
            if (itemTypeAttribute is not null)
            {
                itemType = Type.GetType(itemTypeAttribute.Value);
            }
            if (property is null)
            {
                throw new DronTypeGuidanceException();
            }
            if (property is not null)
            {
                switch (propertyType)
                {
                    case Type type when (
                        type.IsGenericType
                        && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    ):
                        return DeserializeDronListToList(dronList, property, propertyType, obj, itemType: itemType);
                    case Type type when type.IsArray:
                        return DeserializeDronListToArray(dronList, property, propertyType, obj, itemType: itemType);
                    default:
                        throw new DronUnsupportedIEnumerableTypeException(propertyType);
                }
            }
            return dronList.Items.Select(item => _deserializer.DeserializeNode(item));
        }
        #endregion

        #endregion

        #region Private

        #region Member Methods
        private IList ConvertDronListToList(
            DronList dronList,
            Type propertyType,
            Type itemType = null
        )
        {
            if (itemType is null)
            {
                itemType = propertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
            }
            var listType = typeof(List<>).MakeGenericType(new Type[] { itemType });
            var castItems = Activator.CreateInstance(listType) as IList;
            foreach (var item in dronList.Items)
            {
                castItems.Add(
                    _deserializer.DeserializeNode(item, typeOverride: itemType)
                );
            }
            return castItems;
        }

        private IEnumerable DeserializeDronListToList(
            DronList dronList,
            PropertyInfo property,
            Type propertyType,
            object obj = null,
            Type itemType = null
        )
        {
            var castItems = ConvertDronListToList(dronList, propertyType, itemType: itemType);
            if (obj is not null)
            {
                property?.SetValue(obj, castItems);
            }
            return castItems;
        }

        private IEnumerable DeserializeDronListToArray(
            DronList dronList,
            PropertyInfo property,
            Type propertyType,
            object obj = null,
            Type itemType = null
        )
        {
            if (itemType is null)
            {
                itemType = propertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
            }
            var castItems = ConvertDronListToList(dronList, propertyType);
            var arr = Activator.CreateInstance(itemType.MakeArrayType(), castItems.Count) as Array;
            castItems.CopyTo(arr, 0);
            if (obj is not null)
            {
                property?.SetValue(obj, arr);
            }
            return arr;
        }
        #endregion

        #endregion
    }
}