using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRON.Parse;

namespace DRON.Serialization
{
    internal class ListDeserializer : DeserializerBase<DronList>
    {
        #region Internal

        #region Member Methods
        internal override object Deserialize(
            DronList dronList,
            PropertyInfo property = null,
            object obj = null,
            Type _typeOverride = null
        )
        {
            if (property is not null)
            {
                switch (property.PropertyType)
                {
                    case Type type when (
                        type.IsGenericType
                        && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    ):
                        return DeserializeDronListToList(dronList, property, obj);
                    case Type type when type.IsArray:
                        return DeserializeDronListToArray(dronList, property, obj);
                    default:
                        throw new Exception($"Unsupported IEnumerable '{property.PropertyType.Name}'");
                }
            }
            return dronList.Items.Select(item => Deserializer.DeserializeNode(item));
        }
        #endregion

        #endregion

        #region Private

        #region Member Methods
        private static IList ConvertDronListToList(
            DronList dronList,
            PropertyInfo property
        )
        {
            var itemType = property.PropertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
            var listType = typeof(List<>).MakeGenericType(new Type[] { itemType });
            var castItems = Activator.CreateInstance(listType) as IList;
            foreach (var item in dronList.Items)
            {
                castItems.Add(
                    Deserializer.DeserializeNode(item, typeOverride: itemType)
                );
            }
            return castItems;
        }

        private static IEnumerable DeserializeDronListToList(
            DronList dronList,
            PropertyInfo property,
            object obj = null
        )
        {
            var castItems = ConvertDronListToList(dronList, property);
            if (obj is not null)
            {
                property.SetValue(obj, castItems);
            }
            return castItems;
        }

        private static IEnumerable DeserializeDronListToArray(
            DronList dronList,
            PropertyInfo property,
            object obj = null
        )
        {
            var itemType = property.PropertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
            var castItems = ConvertDronListToList(dronList, property);
            var arr = Activator.CreateInstance(itemType.MakeArrayType(), castItems.Count) as Array;
            castItems.CopyTo(arr, 0);
            if (obj is not null)
            {
                property.SetValue(obj, arr);
            }
            return arr;
        }
        #endregion

        #endregion
    }
}