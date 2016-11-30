using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;
using Protobuf.Data;
using ResetCore.Event;

namespace ResetCore.NetPost
{
    public class NetSceneManager : MonoSingleton<NetSceneManager>
    {
        /// <summary>
        /// 场景是否成功连接
        /// </summary>
        private bool _sceneConnected = false;
        public bool sceneConnected
        {
            get
            {
                return _sceneConnected;
            }
            set
            {
                _sceneConnected = value;
                if(_sceneConnected == true)
                {
                    EventDispatcher.TriggerEvent(NetSceneEvent.NetSceneReady);
                }
                else
                {
                    EventDispatcher.TriggerEvent(NetSceneEvent.NetSceneDisconnect);
                }
            }
        }

        /// <summary>
        /// 当前服务器
        /// </summary>
        public BaseServer currentServer { get; private set; }

        /// <summary>
        /// 当前场景Id
        /// </summary>
        public int currentSceneId { get; private set; }

        /// <summary>
        /// 客户端创建的Net
        /// </summary>
        private Dictionary<int, NetBehavior> clientNetBehaviorDict = new Dictionary<int, NetPost.NetBehavior>();  

        public void StartScene(int sceneId)
        {
            currentSceneId = sceneId;
            currentServer = new BaseServer();
            currentServer.Connect(ServerConst.ServerAddress, ServerConst.TcpRemotePort, ServerConst.UdpRemotePort, ServerConst.UdpLocalPort, true);
            Int32Data sceneIdData = new Int32Data();
            sceneIdData.Value = sceneId;
            currentServer.Request(HandlerConst.RequestId.RequsetSceneHandler, -1, sceneIdData, SendType.TCP, (reqSceneRes) =>
            {
                if(reqSceneRes.GetValue<BoolData>().Value == true)
                {
                    Debug.logger.Log("请求场景成功");
                    currentServer.Regist(new List<int> { currentSceneId }, new List<int>(), (pkg) =>
                    {
                        Debug.logger.Log("频道注册成功");
                        EventDispatcher.AddEventListener<NetBehavior>(NetSceneEvent.NetBehaviorAddToScene, AddNetBehavior);
                        sceneConnected = true;
                    });
                }
                else
                {
                    Debug.logger.LogError("NetPost", "请求场景失败");
                }
            });
            
        }

        private void AddNetBehavior(NetBehavior behavior)
        {
            clientNetBehaviorDict.Add(behavior.instanceId, behavior);
        }

        void OnDestroy()
        {
            sceneConnected = false;
            currentServer.Disconnect();
            currentServer = null;
        }
    }
}
