﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace ResetCore.Event
{
    /// <summary>
    /// 代表了能够生成事件的函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GenEventable : Attribute
    {
        public string[] eventName { get; private set; }
        public GenEventable(string[] eventName)
        {
            this.eventName = eventName;
        }
    }
    public class EventBehavior
    {
        public static void GenEvent<T>(T mono) where T:MonoBehaviour
        {
            Type monoType = mono.GetType();

            MethodInfo[] methodInfos = monoType.GetMethods();
            HandleMethods<T>(mono, methodInfos);
        }

        public static void ClearEvent(MonoBehaviour mono)
        {
            MonoEventDispatcher.monoEventControllerDict.Remove(mono);
        }

        private static void HandleMethods<T>(T mono, MethodInfo[] methodInfos)
        {
            for(int i = 0; i < methodInfos.Length; i++)
            {
                HandleMethod(mono, methodInfos[i]);
            }
        }

        private static void HandleMethod<T>(T mono, MethodInfo method)
        {
            object[] attrs = method.GetCustomAttributes(typeof(GenEventable), true);
            if (attrs.Length == 0) return;
            GenEventable genEventAttr = attrs[0] as GenEventable;


            ParameterInfo[] paras = method.GetParameters();
            var eventNames = genEventAttr.eventName;

            if(eventNames == null || eventNames.Length == 0)
            {
                eventNames = new string[] { method.Name };
            }

            for (int i = 0; i < eventNames.Length; i++)
            {
                switch (paras.Length)
                {
                    case 0:
                        {
                            EventDispatcher.AddEventListener(eventNames[i],
                                () => { method.Invoke(mono, new object[0]); }, mono);
                        }
                        break;
                    case 1:
                        {
                            EventDispatcher.AddEventListener<object>(eventNames[i],
                                (arg1) => { method.Invoke(mono, new object[] { arg1 }); }, mono);
                        }
                        break;
                    case 2:
                        {
                            EventDispatcher.AddEventListener<object, object>(eventNames[i],
                                (arg1, arg2) => { method.Invoke(mono, new object[] { arg1, arg2 }); }, mono);
                        }
                        break;
                    case 3:
                        {
                            EventDispatcher.AddEventListener<object, object, object>(eventNames[i],
                                (arg1, arg2, arg3) => { method.Invoke(mono, new object[] { arg1, arg2, arg3 }); }, mono);
                        }
                        break;
                    case 4:
                        {
                            EventDispatcher.AddEventListener<object, object, object, object>(eventNames[i],
                                (arg1, arg2, arg3, arg4) => { method.Invoke(mono, new object[] { arg1, arg2, arg3, arg4 }); }, mono);
                        }
                        break;
                    default:
                        {
                            Debug.logger.LogError("Event Gen Error", "The method " + method.Name + " has too much para");
                        }
                        break;
                }
            }
        }
    }
}
