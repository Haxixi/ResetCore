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
using System;
using Protobuf.Data;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    BaseServer server;
    void Awake()
    {

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
