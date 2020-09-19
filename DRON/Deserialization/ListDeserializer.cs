using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Type typeOverride = null
        )
        {
            var propertyType = typeOverride ?? property.PropertyType;
            if (property is null)
            {
                throw new Exception("Must provide type guidance to deserialize DronList");
            }
            if (property is not null)
            {
                switch (propertyType)
                {
                    case Type type when (
                        type.IsGenericType
                        && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    ):
                        return DeserializeDronListToList(dronList, property, propertyType, obj);
                    case Type type when type.IsArray:
                        return DeserializeDronListToArray(dronList, property, propertyType, obj);
                    default:
                        throw new Exception($"Unsupported IEnumerable '{propertyType.Name}'");
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
            Type propertyType
        )
        {
            var itemType = propertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
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
            object obj = null
        )
        {
            var castItems = ConvertDronListToList(dronList, propertyType);
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
            object obj = null
        )
        {
            var itemType = propertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
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