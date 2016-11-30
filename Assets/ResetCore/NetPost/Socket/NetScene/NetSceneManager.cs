using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;

namespace ResetCore.NetPost
{
    public class NetSceneManager : MonoSingleton<NetSceneManager>
    {

        public BaseServer currentServer { get; private set; }
        public int currentSceneId { get; private set; }

        public void StartScene(int sceneId)
        {
            currentSceneId = sceneId;
            currentServer = new BaseServer();
            currentServer.Connect(ServerConst.ServerAddress, ServerConst.TcpRemotePort, ServerConst.UdpRemotePort, ServerConst.UdpLocalPort, true);
            currentServer.Regist(new List<int> { currentSceneId }, new List<int>());
        }
    }
}
