using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    public TestComponent comp;
    


    void Awake()
    {
        //Debug.Log(BuffData.dataMap[1].BuffName);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    void OnDestroy()
    {
       
    }

    public override void Init()
    {
        base.Init();
    }


    void Update()
    {
        
    }
    
}
