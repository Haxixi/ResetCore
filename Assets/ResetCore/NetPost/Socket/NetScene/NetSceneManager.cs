﻿using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;
using Protobuf.Data;
using ResetCore.Event;

namespace ResetCore.NetPost
{
    public sealed class NetSceneManager : Singleton<NetSceneManager>
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
            private set
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

        /// <summary>
        /// 开启场景
        /// </summary>
        /// <param name="sceneId"></param>
        public void StartScene(int sceneId)
        {
            if (sceneConnected)
            {
                Debug.logger.LogError("NetPost", "在连接前请断开连接，当前连接Id为" + currentSceneId);
                return;
            }
            currentSceneId = sceneId;
            if(currentServer == null)
            {
                currentServer = new BaseServer();
            }
            currentServer.Connect(ServerConst.ServerAddress, ServerConst.TcpRemotePort, ServerConst.UdpRemotePort, ServerConst.UdpLocalPort, true);

            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                Int32Data sceneIdData = new Int32Data();
                sceneIdData.Value = sceneId;
                currentServer.Request(HandlerConst.RequestId.RequsetSceneHandler, -1, sceneIdData, SendType.TCP, (reqSceneRes) =>
                {
                    if (reqSceneRes.GetValue<BoolData>().Value == true)
                    {
                        Debug.logger.Log("请求场景成功并且注册至频道");
                        EventDispatcher.AddEventListener<NetBehavior>(NetSceneEvent.NetBehaviorAddToScene, AddNetBehavior);
                        EventDispatcher.AddEventListener<NetBehavior>(NetSceneEvent.NetBehaviorRemoveFromScene, RemoveNetBehavior);
                        sceneConnected = true;
                    }
                    else
                    {
                        Debug.logger.LogError("NetPost", "请求场景失败");
                    }
                }, () =>
                {
                    Debug.logger.LogError("NetPost", "请求场景超时");
                });
            }, 0.5f);

        }

        /// <summary>
        /// 断开场景
        /// </summary>
        public void Disconnect()
        {
            if(sceneConnected == false)
            {
                Debug.logger.LogError("NetPost", "当前不存在连接");
                return;
            }
            sceneConnected = false;
            Int32Data sceneIdData = new Int32Data();
            sceneIdData.Value = currentSceneId;

            ActionQueue destroyQueue = new ActionQueue();
            //添加删除物体的行为
            foreach (var kvp in clientNetBehaviorDict)
            {
                Debug.Log(kvp.Value.gameObject.name);
                destroyQueue.AddAction((act)=> { kvp.Value.RequestDestroy(act); });
            }
            //添加最终断开场景的行为
            destroyQueue.AddAction((act) =>
            {
                currentServer.Request(HandlerConst.RequestId.DisconnectSceneHandler, -1, sceneIdData, SendType.TCP, (pkg) =>
                {
                    if (pkg.GetValue<BoolData>().Value == true)
                    {
                        Debug.logger.Log("成功断开场景");
                    }
                    else
                    {
                        Debug.logger.Log("断开场景失败");
                    }
                    act();
                }, () =>
                {
                    Debug.logger.LogError("NetPost", "断开场景超时");
                });
            });
            destroyQueue.AddAction(OnDestroy);
        }

        private void AddNetBehavior(NetBehavior behavior)
        {
            MonoActionPool.AddAction(() =>
            {
                clientNetBehaviorDict.Add(behavior.instanceId, behavior);
            });
        }

        private void RemoveNetBehavior(NetBehavior behavior)
        {
            MonoActionPool.AddAction(() =>
            {
                clientNetBehaviorDict.Remove(behavior.instanceId);
            });
        }

        void OnDestroy()
        {
            EventDispatcher.RemoveEventListener<NetBehavior>(NetSceneEvent.NetBehaviorAddToScene, AddNetBehavior);
            EventDispatcher.RemoveEventListener<NetBehavior>(NetSceneEvent.NetBehaviorRemoveFromScene, RemoveNetBehavior);
            currentServer.Disconnect();
        }
    }
}