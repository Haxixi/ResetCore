using UnityEngine;
using System.Collections;
using ResetCore.Asset;
using ResetCore.Util;
using System.Collections.Generic;
using System.IO;
using ResetCore.Data.GameDatas.Json;
using ResetCore.Data;
using ResetCore.Json;


//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {


    void Awake()
    {
        Init();
    }
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 3);
        int i = 0;
        CoroutineTaskManager.Instance.LoopTodoByTime(() =>
        {
            i++;
            Debug.Log(i);
        }, 1, -1, gameObject);
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
