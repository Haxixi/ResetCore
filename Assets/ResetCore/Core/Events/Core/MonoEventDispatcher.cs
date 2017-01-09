using UnityEngine;
using System.Collections;
using ResetCore.Event;
using System.Collections.Generic;
using System;
using ResetCore.Util;

namespace ResetCore.Event
{
    public class MonoEventDispatcher
    {

        public static Dictionary<object, EventController> monoEventControllerDict = new Dictionary<object, EventController>();
        /// <summary>
        /// 获得监听物体
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static EventController GetMonoController(object gameObject)
        {
            if (gameObject == null || gameObject.Equals(null))
            {
                Debug.Log("未找到MonoController");
                RemoveMonoController(gameObject);
                return null;
            }

            if (!monoEventControllerDict.ContainsKey(gameObject))
            {
                monoEventControllerDict.Add(gameObject, new EventController());
            }
            return monoEventControllerDict[gameObject];
        }

        /// <summary>
        /// 移除特定监听物体
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RemoveMonoController(object gameObject)
        {
            monoEventControllerDict.Remove(gameObject);
        }

        /// <summary>
        /// 对所有的MonoController
        /// </summary>
        /// <param name="act"></param>
        public static void DoToAllMonoContorller(Action<EventController> act)
        {
            List<object> keyTemp = new List<object>(MonoEventDispatcher.monoEventControllerDict.Keys);
            List<EventController> temp = new List<EventController>(MonoEventDispatcher.monoEventControllerDict.Values);
            for (int i = 0; i < temp.Count; i++)
            {
                if (keyTemp[i] == null || keyTemp[i].Equals(null))
                {
                    RemoveMonoController(keyTemp[i]);
                    continue;
                }
                act(temp[i]);
            }
        }
    }

    public static class MonoEventEx
    {
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="bindObject"></param>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener(this GameObject bindObject, string eventType, Action handler)
        {
            if (bindObject == null) return;
            MonoEventDispatcher.GetMonoController(bindObject).AddEventListener(eventType, handler);
            bindObject.GetOrCreateComponent<MonoEventCleanUp>();
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="bindObject"></param>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener<T>(this GameObject bindObject, string eventType, Action<T> handler)
        {
            if (bindObject == null) return;
            MonoEventDispatcher.GetMonoController(bindObject).AddEventListener<T>(eventType, handler);
            bindObject.GetOrCreateComponent<MonoEventCleanUp>();
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="bindObject"></param>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener<T, U>(this GameObject bindObject, string eventType, Action<T, U> handler)
        {
            if (bindObject == null) return;
            MonoEventDispatcher.GetMonoController(bindObject).AddEventListener<T, U>(eventType, handler);
            bindObject.GetOrCreateComponent<MonoEventCleanUp>();
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="bindObject"></param>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener<T, U, V>(this GameObject bindObject, string eventType, Action<T, U, V> handler)
        {
            if (bindObject == null) return;
            MonoEventDispatcher.GetMonoController(bindObject).AddEventListener<T, U, V>(eventType, handler);
            bindObject.GetOrCreateComponent<MonoEventCleanUp>();
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="bindObject"></param>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public static void AddEventListener<T, U, V, W>(this GameObject bindObject, string eventType, Action<T, U, V, W> handler)
        {
            if (bindObject == null) return;
            MonoEventDispatcher.GetMonoController(bindObject).AddEventListener<T, U, V, W>(eventType, handler);
            bindObject.GetOrCreateComponent<MonoEventCleanUp>();
        }
    }

}

