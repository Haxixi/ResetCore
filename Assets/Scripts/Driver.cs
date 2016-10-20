using UnityEngine;
using System.Collections;
using ResetCore.Asset;
using ResetCore.Util;
using System.Collections.Generic;
using System.IO;
using ResetCore.Data.GameDatas.Json;
using ResetCore.Data;
using ResetCore.Json;
using ResetCore.ScriptObj;
using ResetCore.Data.GameDatas.Obj;
using ResetCore.UGUI;
using ResetCore.Data.GameDatas.Xml;
using ResetCore.Event;
using ResetCore.MySQL;
using ResetCore.NetPost;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    TcpSocket tcpSocket = new TcpSocket();

    void Awake()
    {
        tcpSocket.onReveive += new TcpSocketReceiveDelegate(TestReveive);
        tcpSocket.onSend += new TcpSocketSendDelegate(TestSend);
        tcpSocket.Connect("127.0.0.1", 9999);
        CoroutineTaskManager.Instance.LoopTodoByTime(() =>
        {
            tcpSocket.Send(System.Text.Encoding.UTF8.GetBytes("Test Message"));
            tcpSocket.BeginReceive();
        }, 1, -1);
    }
    // Use this for initialization
    void Start()
    {
    }

    void OnDestroy()
    {
        tcpSocket.Disconnect();
    }

    public void TestReveive(int len, byte[] bytes)
    {
        Debug.LogError("发送" + System.Text.Encoding.UTF8.GetString(bytes));
    }

    public void TestSend(int len)
    {
        Debug.LogError("发送长度为" + len);
    }

    public override void Init()
    {
        base.Init();
    }

    //private List<GameObject> cubes = new List<GameObject>();

    void Update()
    {
        
    }
    
}
