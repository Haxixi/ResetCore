using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.Util;
using Protobuf.Data;
using ResetCore.Event;
using System;

namespace ResetCore.NetPost
{
    /// <summary>
    /// 包含了所有网络物体需要的基本属性
    /// </summary>
    public abstract class NetBehavior : MonoBehaviour
    {
        /// <summary>
        /// 处理的Id
        /// </summary>
        public abstract HandlerConst.RequestId handlerId { get; }

        /// <summary>
        /// 是否是由客户机创建（如玩家角色）
        /// </summary>
        [SerializeField]
        protected bool isClientCreate = false;

        /// <summary>
        /// 物体的InstanceId
        /// </summary>
        [SerializeField]
        private int _instanceId;
        public int instanceId
        {
            get { return _instanceId; }
            set { _instanceId = value; }
        }

        /// <summary>
        /// 是否正与服务端通信
        /// </summary>
        private bool isConnectNetScene = false;

        //用于处理销毁事件的队列
        private static ActionQueue destroyQueue = new ActionQueue();

        public virtual void Awake()
        {
            EventDispatcher.AddEventListener<Package>(NetSceneEvent.GetNetBehaviorEventName(handlerId), OnNetUpdate);
            if (isClientCreate)
            {
                EventDispatcher.AddEventListener(NetSceneEvent.NetSceneReady, RequestObjectJoin);
            }
        }

        void Reset()
        {
            _instanceId = GetHashCode();
        }

        public virtual void Start()
        {
            //如果是客户端创建则自动创建
            if (isClientCreate)
            {
                RequestObjectJoin();
            }
        }

        public virtual void OnEnable(){}

        public virtual void Update(){}

        public virtual void OnDisable(){}

        public virtual void OnDestroy()
        {
            if (isClientCreate)
            {
                EventDispatcher.RemoveEventListener(NetSceneEvent.NetSceneReady, RequestObjectJoin);
            }
            EventDispatcher.RemoveEventListener<Package>(NetSceneEvent.GetNetBehaviorEventName(handlerId), OnNetUpdate);
            if(NetSceneManager.Instance.sceneConnected)
            RequestDestroy();
        }

        /// <summary>
        /// 当收到从服务端发来的消息
        /// </summary>
        /// <param name="serverData"></param>
        public virtual void OnNetUpdate(Package serverPkg){}

        /// <summary>
        /// 请求物体假如场景
        /// </summary>
        private void RequestObjectJoin()
        {
            NetObjectJoinUpData data = new NetObjectJoinUpData();
            data.InstanceId = instanceId;
            data.TypeName = GetType().Name;
            int chnId = NetSceneManager.Instance.currentSceneId;

            if (NetSceneManager.Instance.currentServer != null)
            {
                NetSceneManager.Instance.currentServer
                .Request(HandlerConst.RequestId.NetObjectJoinUpHandler, chnId, data, SendType.TCP, (res) =>
                {

                    BoolData isSucc = res.GetValue<BoolData>();
                    if (isSucc.Value == true)
                    {
                        Debug.logger.Log("成功将NetBehavior加入到场景" + chnId);
                        isConnectNetScene = true;
                        EventDispatcher.TriggerEvent<NetBehavior>(NetSceneEvent.NetBehaviorAddToScene, this);
                    }
                    else
                    {
                        Debug.logger.LogError("NetPost", "NetBehavior加入到场景失败" + chnId);
                    }
                });
            }
        }

        /// <summary>
        /// 将远端对应的物体销毁，脱离服务端,使用内部销毁队列销毁
        /// </summary>
        public void RequestDestroy()
        {
            destroyQueue.AddAction((act)=>RequestDestroy(act));
        }

        /// <summary>
        /// 将远端对应的物体销毁，脱离服务端,使用外部销毁队列销毁
        /// </summary>
        public void RequestDestroy(Action act)
        {
            //如果未处于连接状态则直接返回
            if (!isConnectNetScene)
            {
                return;
            }

            EventDispatcher.TriggerEvent<NetBehavior>(NetSceneEvent.NetBehaviorRemoveFromScene, this);

            Int32Data instanceId = new Int32Data();
            instanceId.Value = this.instanceId;
            int chnId = NetSceneManager.Instance.currentSceneId;
            Debug.Log("请求销毁:" + gameObject.name);
            NetSceneManager.Instance.currentServer
                .Request(HandlerConst.RequestId.NetObjectRemoveHandler, chnId, instanceId, SendType.TCP, (res) =>
                {
                    BoolData isSucc = res.GetValue<BoolData>();
                    if (isSucc.Value)
                    {
                        Debug.logger.Log("成功将NetBehavior:" + gameObject.name + "移除场景 " + chnId);
                    }
                    else
                    {
                        Debug.logger.LogError("NetPost", "将NetBehavior:" + gameObject.name + "移除场景失败 " + chnId);
                    }
                    isConnectNetScene = false;
                    act();
                });
        }

    }

    /// <summary>
    /// 泛型化
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class NetBehavior<T> : NetBehavior
    {
        
        protected T behaviorData;

        /// <summary>
        /// 用于修改当前的NetBehavior属性并且同步到服务器
        /// </summary>
        /// <param name="data"></param>
        public virtual void SetData(T data, bool sendToSerer = true)
        {
            if (NetSceneManager.Instance.sceneConnected == false)
                return;

            int sceneId = NetSceneManager.Instance.currentSceneId;
            NetSceneManager.Instance.currentServer.Send(handlerId, sceneId, data, SendType.UDP);
            behaviorData = data;
        }
    }

}
