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
using System;
using System.Diagnostics;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    public TestComponent comp;
    


    void Awake()
    {
        EventBehavior.GenEvent(this);
        EventDispatcher.AddEventListener<int>("TestComponent.testProp", (x => { UnityEngine.Debug.Log("asd"); }));
    }

    // Use this for initialization
    void Start()
    {
        comp.testProp = 10;
    }

    void OnDestroy()
    {
        EventBehavior.ClearEvent(this);
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
