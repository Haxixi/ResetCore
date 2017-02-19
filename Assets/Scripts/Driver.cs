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
        
    }
    int i = 0;
    // Use this for initialization
    void Start()
    {
        ActionQueue queue = new ActionQueue();
        Action<Action> testAct = (act) => {
            UnityEngine.Debug.Log("haha" + i);
            i++;
            CoroutineTaskManager.Instance.WaitSecondTodo(() => {  act(); }, 0.1f);
        };
        for(int i = 0; i < 1000; i++)
        {
            queue.AddAction(testAct);
        }

        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            queue.Clean();
            UnityEngine.Debug.Log("停止");
        }, 3);

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
