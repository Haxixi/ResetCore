using UnityEngine;
using System.Collections;
using System.Reflection;

namespace ResetCore.Util
{
    public static class ReflectEx
    {
        /// <summary>
        /// 通过反射方式调用函数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeByReflect(this object obj, string methodName, params object[] args)
        {
            MethodInfo methodInfo = obj.GetType().GetMethod(methodName);
            if (methodInfo == null) return null;
            return methodInfo.Invoke(obj, args);
        }

        /// <summary>
        /// 通过反射方式获取域值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName">域名</param>
        /// <returns></returns>
        public static object GetFieldByReflect(this object obj, string fieldName)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(fieldName);
            if (fieldInfo == null) return null;
            return fieldInfo.GetValue(obj);
        }

        /// <summary>
        /// 通过反射方式获取属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName">属性名</param>
        /// <returns></returns>
        public static object GetPropertyByReflect(this object obj, string propertyName, object[] index = null)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo == null) return null;
            return propertyInfo.GetValue(obj, index);
        }
    }

}
