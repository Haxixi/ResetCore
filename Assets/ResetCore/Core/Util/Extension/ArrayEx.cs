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

    /// <summary>
    /// 获取子数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="sourceStart"></param>
    /// <param name="targetStart"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T[] SubArray<T>(this T[] array, int sourceStart, int targetStart, int length)
    {
        T[] res = new T[length];
        Array.Copy(array, sourceStart, res, targetStart, length);
        return res;
    }

    /// <summary>
    /// 连接两个数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="anotherArray"></param>
    /// <returns></returns>
    public static T[] Concat<T>(this T[] array, T[] anotherArray)
    {
        T[] res = new T[array.Length + anotherArray.Length];
        Array.Copy(array, 0, res, 0, array.Length);
        Array.Copy(anotherArray, 0, res, array.Length, anotherArray.Length);
        return res;
    }

}
