using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.Util;
using Protobuf.Data;
using ResetCore.Event;

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
            protected set { _instanceId = value; }
        }

        public virtual void Awake()
        {
            //instanceId = gameObject.GetInstanceID();
            EventDispatcher.AddEventListener<Package>(NetSceneEvent.GetNetBehaviorEventName(handlerId), OnNetUpdate);
            //如果是由客户端创建则自动销毁
            if (isClientCreate)
            {
                EventDispatcher.AddEventListener(NetSceneEvent.NetSceneDisconnect, RequestDestroy);
            }
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
            EventDispatcher.TriggerEvent<NetBehavior>(NetSceneEvent.NetBehaviorRemoveFromScene, this);
            EventDispatcher.RemoveEventListener<Package>(NetSceneEvent.GetNetBehaviorEventName(handlerId), OnNetUpdate);
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
            if (NetSceneManager.Instance.sceneConnected == false)
            {
                EventDispatcher.AddEventListener(NetSceneEvent.NetSceneReady, RequestObjectJoin);
            }
            else
            {
                NetObjectJoinUpData data = new NetObjectJoinUpData();
                data.InstanceId = instanceId;
                data.TypeName = GetType().Name;
                int chnId = NetSceneManager.Instance.currentSceneId;

                if(NetSceneManager.Instance.currentServer != null)
                {
                    NetSceneManager.Instance.currentServer
                    .Request(HandlerConst.RequestId.NetObjectJoinUpHandler, chnId, data, SendType.TCP, (res) =>
                    {
                        EventDispatcher.RemoveEventListener(NetSceneEvent.NetSceneReady, RequestObjectJoin);
                        BoolData isSucc = res.GetValue<BoolData>();
                        if (isSucc.Value == true)
                        {
                            Debug.logger.Log("成功将NetBehavior加入到场景" + chnId);
                            EventDispatcher.TriggerEvent<NetBehavior>(NetSceneEvent.NetBehaviorAddToScene, this);
                        }
                        else
                        {
                            Debug.logger.LogError("NetPost", "NetBehavior加入到场景失败" + chnId);
                        }
                    });
                }
                else
                {
                    Debug.logger.LogError("NetPost", "服务器被提前销毁" + chnId);
                }
                
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void RequestDestroy()
        {
            Int32Data instanceId = new Int32Data();
            instanceId.Value = this.instanceId;
            int chnId = NetSceneManager.Instance.currentSceneId;
            NetSceneManager.Instance.currentServer
                .Request(HandlerConst.RequestId.NetObjectRemoveHandler, chnId, instanceId, SendType.TCP, (res) =>
                {
                    BoolData isSucc = res.GetValue<BoolData>();
                    if (isSucc.Value)
                    {
                        Debug.logger.Log("成功将NetBehavior移除场景" + chnId);
                    }
                    else
                    {
                        Debug.logger.LogError("NetPost", "将NetBehavior移除场景失败" + chnId);
                    }
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
        public virtual void SetData(T data)
        {
            if (NetSceneManager.Instance.sceneConnected == false)
                return;

            int sceneId = NetSceneManager.Instance.currentSceneId;
            NetSceneManager.Instance.currentServer.Send(handlerId, sceneId, data, SendType.UDP);
            behaviorData = data;
        }
    }

}
