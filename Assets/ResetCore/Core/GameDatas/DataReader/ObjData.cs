using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using ResetCore.Data;
using ResetCore.Util;
using System.Linq;

namespace ResetCore.Data.GameDatas.Obj
{
    public class ObjDataBundle:ScriptableObject
    {
        public ArrayList dataArray = new ArrayList();
    }
    
    public class ObjData
    {
        public int id
        {
            get;
            protected set;
        }

        public static readonly string nameSpace = "ResetCore.Data.GameDatas.Obj";
        public static readonly string ex = ".asset";

        protected static Dictionary<int, T> GetDataMap<T>() where T : ObjData<T>
        {
            Type type = typeof(T);
            FieldInfo field = type.GetField("fileName");
            Dictionary<int, T> dictionary = new Dictionary<int, T>();
            if (field != null)
            {
                string fileName = field.GetValue(null) as string;
                dictionary = new ObjDataController().FormatObjData<T>(fileName);
            }
            else
            {
                return new Dictionary<int, T>();
            }
            return dictionary;
        }
    }

    public abstract class ObjData<T> : ObjData where T : ObjData<T>
    {
        private static Dictionary<int, T> m_dataMap;

        public static Dictionary<int, T> dataMap
        {
            get
            {
                if (ObjData<T>.m_dataMap == null)
                {
                    ObjData<T>.m_dataMap = ObjData.GetDataMap<T>();
                }
                return ObjData<T>.m_dataMap;
            }
            set
            {
                ObjData<T>.m_dataMap = value;
            }
        }


        public static T Select(Func<T, bool> condition)
        {
            return ObjData<T>.dataMap.Values.FirstOrDefault((data) =>
            {
                return condition(data);
            });
        }
    }

    public class ObjDataController
    {

        protected readonly string m_fileExtention = ".asset";

        public Dictionary<int, T> FormatObjData<T>(string fileName) where T : ObjData<T>
        {
            T obj = Resources.Load(fileName) as T;

            Dictionary<int, T> dict = new Dictionary<int, T>();
            List<ObjData> dataArray = typeof(T).GetField("dataArray").GetValue(obj) as List<ObjData>;
            dataArray.ForEach((i, data) =>
            {
                dict.Add(i + 1, data as T);
            });

            return dict;
        }

    }
}
