using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil;
using System;

namespace ResetCore.ReAssembly
{
    public static class AssemblyEx
    {
        /// <summary>
        /// 通过属性找到类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<TypeDefinition> FindTypesByAttribute<T>(this AssemblyDefinition assembly)
        {
            var targetTypes = new List<TypeDefinition>();
            foreach (var type in assembly.MainModule.Types)
            {
                if (type.HasCustomAttributes)
                {
                    foreach (var customAttribute in type.CustomAttributes)
                    {
                        if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                        {
                            targetTypes.Add(type);
                        }
                    }
                }
            }
            return targetTypes;
        }

        /// <summary>
        /// 通过类型找到方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<MethodDefinition> FindMethodsByAttribute<T>(this AssemblyDefinition assembly)
        {
            var targetMethods = new List<MethodDefinition>();
            foreach (var type in assembly.MainModule.Types)
            {
                foreach (var method in type.Methods)
                {
                    if (method.HasCustomAttributes)
                    {
                        foreach (var customAttribute in method.CustomAttributes)
                        {
                            if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                            {
                                targetMethods.Add(method);
                            }
                        }
                    }
                }
            }
            return targetMethods;
        }

        /// <summary>
        /// 确定方法是否存在属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool HasCustomAttribute<T>(this MethodDefinition method)
        {
            if (method.HasCustomAttributes)
            {
                foreach (var customAttribute in method.CustomAttributes)
                {
                    if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获得特定的特性
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static CustomAttribute GetCustomAttribute<T>(this MethodDefinition method)
        {
            if (method.HasCustomAttributes)
            {
                foreach (var customAttribute in method.CustomAttributes)
                {
                    if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                    {
                        return customAttribute;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获得特定的特性
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static CustomAttribute GetCustomAttribute<T>(this TypeDefinition method)
        {
            if (method.HasCustomAttributes)
            {
                foreach (var customAttribute in method.CustomAttributes)
                {
                    if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                    {
                        return customAttribute;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 判断类型是否存在属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasCustomAttribute<T>(this TypeDefinition type)
        {
            if (type.HasCustomAttributes)
            {
                foreach (var customAttribute in type.CustomAttributes)
                {
                    if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }

}
