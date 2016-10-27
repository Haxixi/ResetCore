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
using ResetCore.Protobuf;
using System;
using Vector3D;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {


    BaseServer server;
    void Awake()
    {
        server = new BaseServer();
        server.Connect("127.0.0.1", 9000, 9051, 10000, true);

        int i = 0;
        CoroutineTaskManager.Instance.LoopTodoByTime(() =>
        {
            Vector3DData data = new Vector3DData();
            data.X = i;
            data.Y = i;
            data.Z = i;

            server.Send<Vector3DData>((int)HandlerConst.HandlerId.TestHandler, data, SendType.TCP);

            i++;

        }, 1, -1);

        
        //tcpSocket.onReceive += new TcpSocketReceiveDelegate(TestReveive);
        //tcpSocket.onSend += new TcpSocketSendDelegate(TestSend);
        //tcpSocket.Connect("127.0.0.1", 9988);

        //tcpSocket.BeginReceive();
        //int i = 0;
        //CoroutineTaskManager.Instance.LoopTodoByTime(() =>
        //{
        //    i++;
        //    Vector3DData data = new Vector3DData();
        //    data.X = i;
        //    data.Y = i;
        //    data.Z = i;
        //    Package pkg = Package.MakePakage<Vector3DData>(1, data);

        //    tcpSocket.Send(pkg.totalData);
        //}, 0.01f, -1);


        //tcpSocket1.onReceive += new TcpSocketReceiveDelegate(TestReveive);
        //tcpSocket1.onSend += new TcpSocketSendDelegate(TestSend);
        //tcpSocket1.Connect("127.0.0.1", 9988);

        //tcpSocket1.BeginReceive();
        //int i1 = 0;
        //CoroutineTaskManager.Instance.LoopTodoByTime(() =>
        //{
        //    i1++;
        //    //TestProtobuf data = new TestProtobuf() { testData = 1, testString = "asdasd" };
        //    Vector3DData data = new Vector3DData();
        //    data.X = i1;
        //    data.Y = i1;
        //    data.Z = i1;

        //    Package pkg = Package.MakePakage<Vector3DData>(1, data);

        //    tcpSocket1.Send(pkg.totalData);
        //}, 0.02f, -1);

    }
    // Use this for initialization
    void Start()
    {
    }

    void OnDestroy()
    {
        server.Disconnect();
    }

    public void TestReveive(int len, byte[] bytes)
    {
        Debug.LogError("收到" + System.Text.Encoding.UTF8.GetString(bytes));
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
