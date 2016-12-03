using UnityEngine;
using System.Collections;
using ResetCore.NetPost;

public class TestNetUI : MonoBehaviour {

    BaseServer sever;

    void Awake()
    {
        sever = new BaseServer();
    }
	public void OnConnect()
    {
        //sever.Connect(ServerConst.ServerAddress, ServerConst.TcpRemotePort, ServerConst.UdpRemotePort, ServerConst.UdpLocalPort, true);
        NetSceneManager.Instance.StartScene(1);
    }

    public void OnDisconnect()
    {
        //sever.Disconnect();
        NetSceneManager.Instance.Disconnect();
    }
}
