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
        //EventDispatcher.AddEventListener<Vector2>("Test", Test).TakeUntil(1);
    }

    public void Test(Vector2 vec)
    {
        //Debug.LogError("Position " + vec.ToString());
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
        //EventDispatcher.TriggerEvent("Test", (Vector2)Input.mousePosition);
    }
    
}
