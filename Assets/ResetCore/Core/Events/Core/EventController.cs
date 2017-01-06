using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace ResetCore.Event
{
    public class EventController
    {
        /// <summary>
        /// 不变事件不会被清除
        /// </summary>
        private List<string> m_permanentEvents = new List<string>();
        private Dictionary<string, Delegate> m_theRouter = new Dictionary<string, Delegate>();
        public Dictionary<string, Delegate> TheRouter
        {
            get
            {
                return this.m_theRouter;
            }
        }

        #region 添加监听器
        //添加监听器
        public void AddEventListener(string eventType, Action handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.m_theRouter[eventType] = (Action)Delegate.Combine((Action)this.m_theRouter[eventType], handler);
        }
        public void AddEventListener<T>(string eventType, Action<T> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.m_theRouter[eventType] = (Action<T>)Delegate.Combine((Action<T>)this.m_theRouter[eventType], handler);
        }
        public void AddEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.m_theRouter[eventType] = (Action<T, U>)Delegate.Combine((Action<T, U>)this.m_theRouter[eventType], handler);
        }
        public void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.m_theRouter[eventType] = (Action<T, U, V>)Delegate.Combine((Action<T, U, V>)this.m_theRouter[eventType], handler);
        }
        public void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            this.OnListenerAdding(eventType, handler);
            this.m_theRouter[eventType] = (Action<T, U, V, W>)Delegate.Combine((Action<T, U, V, W>)this.m_theRouter[eventType], handler);
        }
        
        //添加提供器
        public void AddSingleProvider<Res>(string eventType, Func<Res> provider)
        {
            if (OnSingleProviderAdding(eventType, provider))
            {
                this.m_theRouter[eventType] =
                    (Func<Res>)Delegate.Combine((Func<Res>)this.m_theRouter[eventType], provider);
            }
        }
        public void AddSingleProvider<A1, Res>(string eventType, Func<A1, Res> provider)
        {
            if (OnSingleProviderAdding(eventType, provider))
            {
                this.m_theRouter[eventType] =
                    (Func<A1, Res>)Delegate.Combine((Func<A1, Res>)this.m_theRouter[eventType], provider);
            }
        }
        public void AddSingleProvider<A1, A2, Res>(string eventType, Func<A1, A2, Res> provider)
        {
            if (OnSingleProviderAdding(eventType, provider))
            {
                this.m_theRouter[eventType] =
                    (Func<A1, A2, Res>)Delegate.Combine((Func<A1, A2, Res>)this.m_theRouter[eventType], provider);
            }
        }
        public void AddSingleProvider<A1, A2, A3, Res>(string eventType, Func<A1, A2, A3, Res> provider)
        {
            if (OnSingleProviderAdding(eventType, provider))
            {
                this.m_theRouter[eventType] =
                    (Func<A1, A2, A3, Res>)Delegate.Combine((Func<A1, A2, A3, Res>)this.m_theRouter[eventType], provider);
            }
        }
        public void AddSingleProvider<A1, A2, A3, A4, Res>(string eventType, Func<A1, A2, A3, A4, Res> provider)
        {
            if (OnSingleProviderAdding(eventType, provider))
            {
                this.m_theRouter[eventType] =
                    (Func<A1, A2, A3, A4, Res>)Delegate.Combine((Func<A1, A2, A3, A4, Res>)this.m_theRouter[eventType], provider);
            }
        }

        //添加提供器
        public void AddMultProvider<Res>(string eventType, Func<Res> provider)
        {
            OnMultProviderAdding(eventType, provider);
            this.m_theRouter[eventType] =
                    (Func<Res>)Delegate.Combine((Func<Res>)this.m_theRouter[eventType], provider);
        }
        public void AddMultProvider<A1, Res>(string eventType, Func<A1, Res> provider)
        {
            OnMultProviderAdding(eventType, provider);
            this.m_theRouter[eventType] =
                    (Func<A1, Res>)Delegate.Combine((Func<A1, Res>)this.m_theRouter[eventType], provider);
        }
        public void AddMultProvider<A1, A2, Res>(string eventType, Func<A1, A2, Res> provider)
        {
            OnMultProviderAdding(eventType, provider);
            this.m_theRouter[eventType] =
                    (Func<A1, A2, Res>)Delegate.Combine((Func<A1, A2, Res>)this.m_theRouter[eventType], provider);
        }
        public void AddMultProvider<A1, A2, A3, Res>(string eventType, Func<A1, A2, A3, Res> provider)
        {
            OnMultProviderAdding(eventType, provider);
            this.m_theRouter[eventType] =
                    (Func<A1, A2, A3, Res>)Delegate.Combine((Func<A1, A2, A3, Res>)this.m_theRouter[eventType], provider);
        }
        public void AddMultProvider<A1, A2, A3, A4, Res>(string eventType, Func<A1, A2, A3, A4, Res> provider)
        {
            OnMultProviderAdding(eventType, provider);
            this.m_theRouter[eventType] =
                    (Func<A1, A2, A3, A4, Res>)Delegate.Combine((Func<A1, A2, A3, A4, Res>)this.m_theRouter[eventType], provider);
        }
        #endregion 添加监听器

        #region 移除监听器
        public void RemoveEventListener<T>(string eventType, Action<T> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.m_theRouter[eventType] = (Action<T>)Delegate.Remove((Action<T>)this.m_theRouter[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }
        public void RemoveEventListener(string eventType, Action handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.m_theRouter[eventType] = (Action)Delegate.Remove((Action)this.m_theRouter[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }
        public void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.m_theRouter[eventType] = (Action<T, U>)Delegate.Remove((Action<T, U>)this.m_theRouter[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }
        public void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.m_theRouter[eventType] = (Action<T, U, V>)Delegate.Remove((Action<T, U, V>)this.m_theRouter[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }
        public void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
        {
            if (this.OnListenerRemoving(eventType, handler))
            {
                this.m_theRouter[eventType] = (Action<T, U, V, W>)Delegate.Remove((Action<T, U, V, W>)this.m_theRouter[eventType], handler);
                this.OnListenerRemoved(eventType);
            }
        }

        //移除单返回提供器
        public void RemoveSingleProvider(string providerType)
        {
            if (OnSingleProviderRemoving(providerType))
            {
                m_theRouter.Remove(providerType);
                OnSingleProviderRemoved(providerType);
            }
        }

        //移除多返回提供器
        public void RemoveMultProvider<Res>(string providerType, Func<Res> provider)
        {
            if (this.OnMultProviderRemoving(providerType, provider))
            {
                this.m_theRouter[providerType] = (Func<Res>)Delegate.Remove((Func<Res>)this.m_theRouter[providerType], provider);
                this.OnMultProviderRemoved(providerType);
            }
        }
        public void RemoveMultProvider<A1, Res>(string providerType, Func<A1, Res> provider)
        {
            if (this.OnMultProviderRemoving(providerType, provider))
            {
                this.m_theRouter[providerType] = (Func<A1, Res>)Delegate.Remove((Func<A1, Res>)this.m_theRouter[providerType], provider);
                this.OnMultProviderRemoved(providerType);
            }
        }
        public void RemoveMultProvider<A1, A2, Res>(string providerType, Func<A1, A2, Res> provider)
        {
            if (this.OnMultProviderRemoving(providerType, provider))
            {
                this.m_theRouter[providerType] = (Func<A1, A2, Res>)Delegate.Remove((Func<A1, A2, Res>)this.m_theRouter[providerType], provider);
                this.OnMultProviderRemoved(providerType);
            }
        }
        public void RemoveMultProvider<A1, A2, A3, Res>(string providerType, Func<A1, A2, A3, Res> provider)
        {
            if (this.OnMultProviderRemoving(providerType, provider))
            {
                this.m_theRouter[providerType] = (Func<A1, A2, A3, Res>)Delegate.Remove((Func<A1, A2, A3, Res>)this.m_theRouter[providerType], provider);
                this.OnMultProviderRemoved(providerType);
            }
        }
        public void RemoveMultProvider<A1, A2, A3, A4, Res>(string providerType, Func<A1, A2, A3, A4, Res> provider)
        {
            if (this.OnMultProviderRemoving(providerType, provider))
            {
                this.m_theRouter[providerType] = (Func<A1, A2, A3, A4, Res>)Delegate.Remove((Func<A1, A2, A3, A4, Res>)this.m_theRouter[providerType], provider);
                this.OnMultProviderRemoved(providerType);
            }
        }
        #endregion

        #region 触发事件
        public void TriggerEvent(string eventType)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action action = invocationList[i] as Action;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action();
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void TriggerEvent<T>(string eventType, T arg1)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T> action = invocationList[i] as Action<T>;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1);
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void TriggerEvent<T, U>(string eventType, T arg1, U arg2)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T, U> action = invocationList[i] as Action<T, U>;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1, arg2);
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void TriggerEvent<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T, U, V> action = invocationList[i] as Action<T, U, V>;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1, arg2, arg3);
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void TriggerEvent<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(eventType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Action<T, U, V, W> action = invocationList[i] as Action<T, U, V, W>;
                    if (action == null)
                    {
                        throw new EventException(string.Format("TriggerEvent {0} error: types of parameters are not match.", eventType));
                    }
                    try
                    {
                        action(arg1, arg2, arg3, arg4);
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }

        //请求单返回请求器
        public Res RequestSingleProvider<Res>(string providerType)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                Func<Res> func = invocationList[0] as Func<Res>;
                return func();
            }
            else
            {
                throw new EventException("未找到对应的提供器");
            }
        }
        public Res RequestSingleProvider<A1, Res>(string providerType, A1 arg1)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                Func<A1, Res> func = invocationList[0] as Func<A1, Res>;
                return func(arg1);
            }
            else
            {
                throw new EventException("未找到对应的提供器");
            }
        }
        public Res RequestSingleProvider<A1, A2, Res>(string providerType, A1 arg1, A2 arg2)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                Func<A1, A2, Res> func = invocationList[0] as Func<A1, A2, Res>;
                return func(arg1, arg2);
            }
            else
            {
                throw new EventException("未找到对应的提供器");
            }
        }
        public Res RequestSingleProvider<A1, A2, A3, Res>(string providerType, A1 arg1, A2 arg2, A3 arg3)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                Func<A1, A2, A3, Res> func = invocationList[0] as Func<A1, A2, A3, Res>;
                return func(arg1, arg2, arg3);
            }
            else
            {
                throw new EventException("未找到对应的提供器");
            }
        }
        public Res RequestSingleProvider<A1, A2, A3, A4, Res>(string providerType, A1 arg1, A2 arg2, A3 arg3, A4 arg4)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                Func<A1, A2, A3, A4, Res> func = invocationList[0] as Func<A1, A2, A3, A4, Res>;
                return func(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new EventException("未找到对应的提供器");
            }
        }

        //请求多返回提供器
        public void RequestMultProvider<Res>(string providerType, Action<Res> handler)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Func<Res> func = invocationList[i] as Func<Res>;
                    if (func == null)
                    {
                        throw new EventException(string.Format("请求多值提供器 {0} 错误: 参数错误.", providerType));
                    }
                    try
                    {
                        handler(func());
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void RequestMultProvider<A1, Res>(string providerType, Action<Res> handler, A1 arg1)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Func<A1, Res> func = invocationList[i] as Func<A1, Res>;
                    if (func == null)
                    {
                        throw new EventException(string.Format("请求多值提供器 {0} 错误: 参数错误.", providerType));
                    }
                    try
                    {
                        handler(func(arg1));
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void RequestMultProvider<A1, A2, Res>(string providerType, Action<Res> handler, A1 arg1, A2 arg2)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Func<A1, A2, Res> func = invocationList[i] as Func<A1, A2, Res>;
                    if (func == null)
                    {
                        throw new EventException(string.Format("请求多值提供器 {0} 错误: 参数错误.", providerType));
                    }
                    try
                    {
                        handler(func(arg1, arg2));
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void RequestMultProvider<A1, A2, A3, Res>(string providerType, Action<Res> handler, A1 arg1, A2 arg2, A3 arg3)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Func<A1, A2, A3, Res> func = invocationList[i] as Func<A1, A2, A3, Res>;
                    if (func == null)
                    {
                        throw new EventException(string.Format("请求多值提供器 {0} 错误: 参数错误.", providerType));
                    }
                    try
                    {
                        handler(func(arg1, arg2, arg3));
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        public void RequestMultProvider<A1, A2, A3, A4, Res>(string providerType, Action<Res> handler, A1 arg1, A2 arg2, A3 arg3, A4 arg4)
        {
            Delegate delegate2;
            if (this.m_theRouter.TryGetValue(providerType, out delegate2))
            {
                Delegate[] invocationList = delegate2.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    Func<A1, A2, A3, A4, Res> func = invocationList[i] as Func<A1, A2, A3, A4, Res>;
                    if (func == null)
                    {
                        throw new EventException(string.Format("请求多值提供器 {0} 错误: 参数错误.", providerType));
                    }
                    try
                    {
                        handler(func(arg1, arg2, arg3, arg4));
                    }
                    catch (Exception exception)
                    {
                        Debug.logger.LogException(exception);
                    }
                }
            }
        }
        #endregion

        #region 公开方法
        //移除某个委托队列
        public void RemoveEvent(string eventType)
        {
            if (m_theRouter.ContainsKey(eventType))
            {
                var temp = m_theRouter[eventType];
                m_theRouter.Remove(eventType);
                temp = null;
            }
        }
        public void CleanUp()
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Delegate> pair in this.m_theRouter)
            {
                bool flag = false;
                foreach (string str in this.m_permanentEvents)
                {
                    if (pair.Key == str)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    list.Add(pair.Key);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                this.m_theRouter.Remove(list[i]);
            }
        }
        public void CleanUp(string eventName)
        {
            List<string> list = new List<string>();
            bool flag = false;
            foreach (string str in this.m_permanentEvents)
            {
                if (eventName == str)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                list.Add(eventName);
            }
            for (int i = 0; i < list.Count; i++ )
            {
                this.m_theRouter.Remove(list[i]);
            }
        }
        public bool ContainsEvent(string eventType)
        {
            return this.m_theRouter.ContainsKey(eventType);
        }
        //设为常驻事件
        public void MarkAsPermanent(string eventType)
        {
            this.m_permanentEvents.Add(eventType);
        }
        #endregion

        #region 私有方法
        //移除普通事件
        private void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
        {
            if (!this.m_theRouter.ContainsKey(eventType))
            {
                this.m_theRouter.Add(eventType, null);
            }
            Delegate delegate2 = this.m_theRouter[eventType];
            if ((delegate2 != null) && (delegate2.GetType() != listenerBeingAdded.GetType()))
            {
                throw new EventException(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, delegate2.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }
        private void OnListenerRemoved(string eventType)
        {
            if (this.m_theRouter.ContainsKey(eventType) && (this.m_theRouter[eventType] == null))
            {
                this.m_theRouter.Remove(eventType);
            }
        }
        private bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
        {
            if (!this.m_theRouter.ContainsKey(eventType))
            {
                return false;
            }
            Delegate delegate2 = this.m_theRouter[eventType];
            if ((delegate2 != null) && (delegate2.GetType() != listenerBeingRemoved.GetType()))
            {
                throw new EventException(string.Format("Remove listener {0}\" failed, Current type is {1}, adding type is {2}.", eventType, delegate2.GetType(), listenerBeingRemoved.GetType()));
            }
            return true;
        }
        //移除单返回提供器
        private bool OnSingleProviderAdding(string providerType, Delegate providerBeingAdded)
        {
            if (!this.m_theRouter.ContainsKey(providerType))
            {
                this.m_theRouter.Add(providerType, null);
                Delegate delegate2 = this.m_theRouter[providerType];
                return true;
            }
            else
            {
                throw new EventException("一个SingleProvider只能绑定一个方法，当前方法名为" + providerType);
            }
        }
        private bool OnSingleProviderRemoving(string providerType)
        {
            if (!this.m_theRouter.ContainsKey(providerType))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void OnSingleProviderRemoved(string providerType)
        {
            OnListenerRemoved(providerType);
        }
        //移除多返回提供器
        private void OnMultProviderAdding(string providerType, Delegate providerBeingAdded)
        {
            //实现与Listener一致
            OnListenerAdding(providerType, providerBeingAdded);
        }
        private bool OnMultProviderRemoving(string providerType, Delegate providerBeingAdded)
        {
            if (!this.m_theRouter.ContainsKey(providerType))
            {
                return false;
            }
            Delegate delegate2 = this.m_theRouter[providerType];
            if ((delegate2 != null) && (delegate2.GetType() != providerBeingAdded.GetType()))
            {
                throw new EventException(string.Format("移除提供器 {0}\" 失败, 当前类型是 {1}, 移除类型是 {2}.", 
                    providerType, delegate2.GetType(), providerType.GetType()));
            }
            return true;
        }
        private void OnMultProviderRemoved(string providerType)
        {
            OnListenerRemoved(providerType);
        }
        #endregion

    }

    public class EventException : Exception
    {

        public EventException(string message)
            : base(message)
        {
        }

        public EventException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
