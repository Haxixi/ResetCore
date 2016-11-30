using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.Util;
using Protobuf.Data;
using ResetCore.Event;

namespace ResetCore.NetPost
{
    public class NetBehavior<T> : MonoBehaviour
    {
        public int instanceId { get; private set; }

        private T behaviorData;

        public virtual void Awake()
        {
            instanceId = gameObject.GetInstanceID();
            EventDispatcher.AddEventListener<T>("NetBehavior" + instanceId, OnNetUpdate);
        }

        public virtual void Start()
        {
            NetObjectJoinUpData data = new NetObjectJoinUpData();
            data.InstanceId = instanceId;
            data.TypeName = GetType().Name;
            NetSceneManager.Instance.currentServer.Send(HandlerConst.RequestId.NetObjectJoinUpHandler, NetSceneManager.Instance.currentSceneId, data, SendType.TCP);
        }

        public virtual void OnEnable()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnDestroy()
        {
            EventDispatcher.RemoveEventListener<T>("NetBehavior" + instanceId, OnNetUpdate);
        }

        /// <summary>
        /// 当收到从服务端发来的消息
        /// </summary>
        /// <param name="serverData"></param>
        public virtual void OnNetUpdate(T serverData)
        {

        }
        
    }

}
