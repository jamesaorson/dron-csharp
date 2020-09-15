using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DRON.Parse;

namespace DRON.Deserialization
{
    internal class DictDeserializer : DeserializerBase<DronObject>
    {
        #region Internal

        #region Member Methods
        internal override object Deserialize(
            DronObject dronObject,
            PropertyInfo property = null,
            object obj = null,
            Type typeOverride = null
        )
        {
            var propertyType = typeOverride is not null
                ? typeOverride : property?.PropertyType;
            if (propertyType is null)
            {
                throw new Exception("Must provide type guidance to deserialize a dictionary");
            }
            var typeArguments = propertyType.GetGenericArguments();
            var keyType = typeArguments[0];
            var valueType = typeArguments[1];
            var dictType = typeof(Dictionary<,>).MakeGenericType(
                keyType,
                valueType
            );
            var dict = Activator.CreateInstance(dictType) as IDictionary;
            foreach (var field in dronObject.Fields)
            {
                var key = StringDeserializer.ConvertString(field.Key, keyType);
                dict[key] = Deserializer.DeserializeNode(
                    field.Value.Value,
                    typeOverride: valueType
                );
            }
            return dict;
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