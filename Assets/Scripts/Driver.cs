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

    public GameObject cube;
    void Awake()
    {
        cube = GameObject.Find("Cube");
        EventDispatcher.AddEventListener("heihei", ()=> { Debug.LogError("Before Delete"); }, cube);
        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            EventDispatcher.TriggerEvent("heihei");
            Destroy(cube);
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                Debug.LogError("AfterDelete");
                EventDispatcher.TriggerEvent("heihei");
            }, 1f);
        }, 1f);
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
