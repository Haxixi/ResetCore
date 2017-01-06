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
using ResetCore.NetPost;
using ResetCore.Protobuf;
using Protobuf.Data;
using ResetCore.ReAssembly;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    void Awake()
    {
        EventDispatcher.AddMultProvider("Test", () =>
        {
            return 100;
        });
        EventDispatcher.AddMultProvider("Test", () =>
        {
            return 2;
        });
        EventDispatcher.RequestMultProvider<int>("Test", (res) =>
        {
            Debug.logger.Log(res);
        });
    }
    // Use this for initialization
    void Start()
    {
    }

    void OnDestroy()
    {
        //server.Disconnect();
    }

    public override void Init()
    {
        base.Init();
    }


    void Update()
    {
        
    }
    
}
