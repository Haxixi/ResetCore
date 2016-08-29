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


//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {


    void Awake()
    {
        Init();
    }
    // Use this for initialization
    void Start()
    {
        //new ResDownloadManager().CheckVersion((str) =>
        //{
        //    Debug.Log("info:" + str);
        //}, (progress) =>
        //{
        //    Debug.Log("progress:" + progress);
        //}, (comp) =>
        //{
        //    Debug.Log(comp ? "Finish" : "Fail");
        //}, (ver) =>
        //{
        //    Debug.Log("Need Update newset is " + ver.ToString());
        //}, (ex) =>
        //{
        //    Debug.LogException(ex);
        //});

        //gameObject.GetComponent<>

        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            LanguageManager.SetLanguageType(LanguageConst.LanguageType.English);
        }, 3);
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
