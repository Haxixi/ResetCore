using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.Util;
using System.Reflection;
using System;

namespace ResetCore.Data
{
    public class DataUtil
    {
        /// <summary>
        /// 将Dictionary<int, Dictionary<string, string>>转成Dictionary<int, T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Dictionary<int, T> ParserStringDict2ClassDict<T>(Dictionary<int, Dictionary<string, string>> dictionary)
        {
            Dictionary<int, T> dataDic = new Dictionary<int, T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (KeyValuePair<int, Dictionary<string, string>> pair in dictionary)
            {
                T propInstance = Activator.CreateInstance<T>();
                PropertyInfo[] array = properties;
                for (int i = 0; i < array.Length; i++)
                {
                    PropertyInfo propInfo = array[i];
                    if (propInfo.Name == "id")
                    {
                        //Key值为序号
                        propInfo.SetValue(propInstance, pair.Key, null);
                    }
                    else if (pair.Value.ContainsKey(propInfo.Name))
                    {
                        object propValue = StringEx.GetValue(pair.Value[propInfo.Name], propInfo.PropertyType);
                        propInfo.SetValue(propInstance, propValue, null);
                    }
                    else
                    {
                        Debug.logger.LogError("Add New Value", propInfo.Name + "Not in the Xml");
                    }
                }
                dataDic.Add(pair.Key, propInstance);
            }
            return dataDic;
        }
    }

}
