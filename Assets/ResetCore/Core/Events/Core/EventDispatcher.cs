using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace ResetCore.Event
{
    public class EventDispatcher
    {
        private static EventController m_eventController = new EventController();
        //全局监听器
        public static Dictionary<string, Delegate> TheRouter
        {
            get
            {
                return m_eventController.TheRouter;
            }
        }

        #region 添加监听器(使用物体
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void AddEventListener(string eventType, Action handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddEventListener(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if(controller != null)
                    controller.AddEventListener(eventType, handler);
            }

        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void AddEventListener<T>(string eventType, Action<T> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddEventListener<T>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddEventListener<T>(eventType, handler);
            }
            
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void AddEventListener<T, U>(string eventType, Action<T, U> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddEventListener<T, U>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddEventListener<T, U>(eventType, handler);
            }
            
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddEventListener<T, U, V>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddEventListener<T, U, V>(eventType, handler);
            }
            
        }
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddEventListener<T, U, V, W>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddEventListener<T, U, V, W>(eventType, handler);
            }
        }

        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddSingleProvider<Res>(string provideType, Func<Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddSingleProvider<Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddSingleProvider<Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddSingleProvider<A1, Res>(string provideType, Func<A1, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddSingleProvider<A1, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddSingleProvider<A1, Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddSingleProvider<A1, A2, Res>(string provideType, Func<A1, A2, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddSingleProvider<A1, A2, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddSingleProvider<A1, A2, Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddSingleProvider<A1, A2, A3, Res>(string provideType, Func<A1, A2, A3, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddSingleProvider<A1, A2, A3, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddSingleProvider<A1, A2, A3, Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddSingleProvider<A1, A2, A3, A4, Res>(string provideType, Func<A1, A2, A3, A4, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddSingleProvider<A1, A2, A3, A4, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddSingleProvider<A1, A2, A3, A4, Res>(provideType, provider);
            }
        }

        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddMultProvider<Res>(string provideType, Func<Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddMultProvider<Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddMultProvider<Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddMultProvider<A1, Res>(string provideType, Func<A1, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddMultProvider<A1, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddMultProvider<A1, Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddMultProvider<A1, A2, Res>(string provideType, Func<A1, A2, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddMultProvider<A1, A2, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddMultProvider<A1, A2, Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddMultProvider<A1, A2, A3, Res>(string provideType, Func<A1, A2, A3, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddMultProvider<A1, A2, A3, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddMultProvider<A1, A2, A3, Res>(provideType, provider);
            }
        }
        /// <summary>
        /// 添加单返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="provideType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void AddMultProvider<A1, A2, A3, A4, Res>(string provideType, Func<A1, A2, A3, A4, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.AddMultProvider<A1, A2, A3, A4, Res>(provideType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.AddMultProvider<A1, A2, A3, A4, Res>(provideType, provider);
            }
        }
        #endregion //添加监听器

        #region 监听器工具
        /// <summary>
        /// 清理监听器
        /// </summary>
        /// <param name="bindObject">绑定对象</param>
        public static void Cleanup(object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.CleanUp();
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.CleanUp();
            }
            
        }

        /// <summary>
        /// 设置为持久
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="bindObject"></param>
        public static void MarkAsPermanent(string eventType, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.MarkAsPermanent(eventType);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.MarkAsPermanent(eventType);
            }
            
        }
        #endregion //监听器工具

        #region 移除监听器

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="bindObject"></param>
        public static void RemoveEvent(string eventType, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveEvent(eventType);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveEvent(eventType);
            }
        }

        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void RemoveEventListener(string eventType, Action handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveEventListener(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveEventListener(eventType, handler);
            }
        }
        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void RemoveEventListener<T>(string eventType, Action<T> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveEventListener<T>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveEventListener<T>(eventType, handler);
            }
                
        }
        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void RemoveEventListener<T, U>(string eventType, Action<T, U> handler, object bindObject = null)
        {
            if(bindObject == null)
            {
                m_eventController.RemoveEventListener<T, U>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveEventListener<T, U>(eventType, handler);
            }
               
        }
        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveEventListener<T, U, V>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveEventListener<T, U, V>(eventType, handler);
            }
        }
        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">处理行为</param>
        /// <param name="bindObject">绑定对象</param>
        public static void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveEventListener<T, U, V, W>(eventType, handler);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveEventListener<T, U, V, W>(eventType, handler);
            }
        }

        /// <summary>
        /// 移除单返回提供器
        /// </summary>
        /// <param name="provider"></param>
        public static void RemoveSingleProvider(string provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveSingleProvider(provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveSingleProvider(provider);
            }
        }

        /// <summary>
        /// 移除多返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void RemoveMultProvider<Res>(string eventType, Func<Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveMultProvider<Res>(eventType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveMultProvider<Res>(eventType, provider);
            }
        }
        /// <summary>
        /// 移除多返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void RemoveMultProvider<A1, Res>(string eventType, Func<A1, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveMultProvider<A1, Res>(eventType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveMultProvider<A1, Res>(eventType, provider);
            }
        }
        /// <summary>
        /// 移除多返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void RemoveMultProvider<A1, A2, Res>(string eventType, Func<A1, A2, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveMultProvider<A1, A2, Res>(eventType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveMultProvider<A1, A2, Res>(eventType, provider);
            }
        }
        /// <summary>
        /// 移除多返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void RemoveMultProvider<A1, A2, A3, Res>(string eventType, Func<A1, A2, A3, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveMultProvider<A1, A2, A3, Res>(eventType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveMultProvider<A1, A2, A3, Res>(eventType, provider);
            }
        }
        /// <summary>
        /// 移除多返回提供器
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="provider"></param>
        /// <param name="bindObject"></param>
        public static void RemoveMultProvider<A1, A2, A3, A4, Res>(string eventType, Func<A1, A2, A3, A4, Res> provider, object bindObject = null)
        {
            if (bindObject == null)
            {
                m_eventController.RemoveMultProvider<A1, A2, A3, A4, Res>(eventType, provider);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(bindObject);
                if (controller != null)
                    controller.RemoveMultProvider<A1, A2, A3, A4, Res>(eventType, provider);
            }
        }

        #endregion //移除监听器、

        #region 触发事件
        /// <summary>
        /// 触发行为
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="triggerObject">触发对象</param>
        public static void TriggerEvent(string eventType, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.TriggerEvent(eventType);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.TriggerEvent(eventType);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.TriggerEvent(eventType);
            }
            
        }
        /// <summary>
        /// 触发行为
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="triggerObject">触发对象</param>
        public static void TriggerEvent<T>(string eventType, T arg1, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.TriggerEvent<T>(eventType, arg1);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.TriggerEvent<T>(eventType, arg1);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.TriggerEvent<T>(eventType, arg1);
            }
            
        }
        /// <summary>
        /// 触发行为
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="triggerObject">触发对象</param>
        public static void TriggerEvent<T, U>(string eventType, T arg1, U arg2, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.TriggerEvent<T, U>(eventType, arg1, arg2);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.TriggerEvent<T, U>(eventType, arg1, arg2);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.TriggerEvent<T, U>(eventType, arg1, arg2);
            }
            
        }
        /// <summary>
        /// 触发行为
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="triggerObject">触发对象</param>
        public static void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.TriggerEvent<T, U, V>(eventType, arg1, arg2, arg3);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.TriggerEvent<T, U, V>(eventType, arg1, arg2, arg3);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.TriggerEvent<T, U, V>(eventType, arg1, arg2, arg3);
            }
            
        }
        /// <summary>
        /// 触发行为
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="triggerObject">触发对象</param>
        public static void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.TriggerEvent<T, U, V, W>(eventType, arg1, arg2, arg3, arg4);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.TriggerEvent<T, U, V, W>(eventType, arg1, arg2, arg3, arg4);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.TriggerEvent<T, U, V, W>(eventType, arg1, arg2, arg3, arg4);
            }

        }
        
        /// <summary>
        /// 请求单返回提供器返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static Res RequestSingleProvider<Res>(string providerType, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                return m_eventController.RequestSingleProvider<Res>(providerType);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    return controller.RequestSingleProvider<Res>(providerType);
                else
                    return default(Res);
            }
        }
        /// <summary>
        /// 请求单返回提供器返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static Res RequestSingleProvider<A1, Res>(string providerType, A1 arg1, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                return m_eventController.RequestSingleProvider<A1, Res>(providerType, arg1);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    return controller.RequestSingleProvider<A1, Res>(providerType, arg1);
                else
                    return default(Res);
            }
        }
        /// <summary>
        /// 请求单返回提供器返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static Res RequestSingleProvider<A1, A2, Res>(string providerType, A1 arg1, A2 arg2, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                return m_eventController.RequestSingleProvider<A1, A2, Res>(providerType, arg1, arg2);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    return controller.RequestSingleProvider<A1, A2, Res>(providerType, arg1, arg2);
                else
                    return default(Res);
            }
        }
        /// <summary>
        /// 请求单返回提供器返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static Res RequestSingleProvider<A1, A2, A3, Res>(string providerType, A1 arg1, A2 arg2, A3 arg3, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                return m_eventController.RequestSingleProvider<A1, A2, A3, Res>(providerType, arg1, arg2, arg3);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    return controller.RequestSingleProvider<A1, A2, A3, Res>(providerType, arg1, arg2, arg3);
                else
                    return default(Res);
            }
        }
        /// <summary>
        /// 请求单返回提供器返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static Res RequestSingleProvider<A1, A2, A3, A4, Res>(string providerType, A1 arg1, A2 arg2, A3 arg3, A4 arg4, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                return m_eventController.RequestSingleProvider<A1, A2, A3, A4, Res>(providerType, arg1, arg2, arg3, arg4);
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    return controller.RequestSingleProvider<A1, A2, A3, A4, Res>(providerType, arg1, arg2, arg3, arg4);
                else
                    return default(Res);
            }
        }


        /// <summary>
        /// 请求并处理多返回提供器的返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <param name="handler"></param>
        /// <param name="triggerObject"></param>
        public static void RequestMultProvider<Res>(string providerType, Action<Res> handler, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.RequestMultProvider<Res>(providerType, handler);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.RequestMultProvider<Res>(providerType, handler);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.RequestMultProvider<Res>(providerType, handler);
            }

        }
        /// <summary>
        /// 请求并处理多返回提供器的返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <param name="handler"></param>
        /// <param name="triggerObject"></param>
        public static void RequestMultProvider<A1, Res>(string providerType, Action<Res> handler, A1 arg1, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.RequestMultProvider<A1, Res>(providerType, handler, arg1);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.RequestMultProvider<A1, Res>(providerType, handler, arg1);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.RequestMultProvider<A1, Res>(providerType, handler, arg1);
            }

        }
        /// <summary>
        /// 请求并处理多返回提供器的返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <param name="handler"></param>
        /// <param name="triggerObject"></param>
        public static void RequestMultProvider<A1, A2, Res>(string providerType, Action<Res> handler, A1 arg1, A2 arg2, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.RequestMultProvider<A1, A2, Res>(providerType, handler, arg1, arg2);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.RequestMultProvider<A1, A2, Res>(providerType, handler, arg1, arg2);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.RequestMultProvider<A1, A2, Res>(providerType, handler, arg1, arg2);
            }

        }
        /// <summary>
        /// 请求并处理多返回提供器的返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <param name="handler"></param>
        /// <param name="triggerObject"></param>
        public static void RequestMultProvider<A1, A2, A3, Res>(string providerType, Action<Res> handler, A1 arg1, A2 arg2, A3 arg3, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.RequestMultProvider<A1, A2, A3, Res>(providerType, handler, arg1, arg2, arg3);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.RequestMultProvider<A1, A2, A3, Res>(providerType, handler, arg1, arg2, arg3);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.RequestMultProvider<A1, A2, A3, Res>(providerType, handler, arg1, arg2, arg3);
            }

        }
        /// <summary>
        /// 请求并处理多返回提供器的返回消息
        /// </summary>
        /// <typeparam name="Res"></typeparam>
        /// <param name="providerType"></param>
        /// <param name="handler"></param>
        /// <param name="triggerObject"></param>
        public static void RequestMultProvider<A1, A2, A3, A4, Res>(string providerType, Action<Res> handler, A1 arg1, A2 arg2, A3 arg3, A4 arg4, object triggerObject = null)
        {
            if (triggerObject == null)
            {
                m_eventController.RequestMultProvider<A1, A2, A3, A4, Res>(providerType, handler, arg1, arg2, arg3, arg4);
                MonoEventDispatcher.DoToAllMonoContorller((con) =>
                {
                    con.RequestMultProvider<A1, A2, A3, A4, Res>(providerType, handler, arg1, arg2, arg3, arg4);
                });
            }
            else
            {
                EventController controller = MonoEventDispatcher.GetMonoController(triggerObject);
                if (controller != null)
                    controller.RequestMultProvider<A1, A2, A3, A4, Res>(providerType, handler, arg1, arg2, arg3, arg4);
            }

        }
        #endregion //触发事件

        

    }

}

