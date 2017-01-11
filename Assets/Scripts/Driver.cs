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
        gameObject.AddEventListener<ArrayList>("Request", (array) =>
        {
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                SyncRequester.Response("Response", new ArrayList() { array.Count.ToString() });
            }, 5);
        });

        SyncRequester.Request("Request", new ArrayList() { "TestData" }, "Response", (array) =>
        {
            Debug.Log(array[0] as string);
        }, 1000);
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
