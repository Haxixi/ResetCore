using UnityEngine;
using System.Collections;
using System;

public static class ArrayEx {

    /// <summary>
    /// 遍历数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="act"></param>
	public static void Foreach<T>(this Array array, Action<int, T> act)
    {
        for(int i = 0; i < array.Length; i++)
        {
            act(i, (T)array.GetValue(i));
        }
    }

    /// <summary>
    /// 遍历数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="act"></param>
    public static void Foreach<T>(this Array array, Action<T> act)
    {
        for (int i = 0; i < array.Length; i++)
        {
            act((T)array.GetValue(i));
        }
    }

    /// <summary>
    /// 数组是否包含特定值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool Contains<T>(this Array array, T obj)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array.GetValue(i).Equals(obj))
            {
                return true;
            }
        }
        return false;
    }


}
