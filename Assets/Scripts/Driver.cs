using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {


    void Awake()
    {
        EventDispatcher.AddEventListener<Vector2>("PoolInTime", vec =>
        {
            Debug.Log(vec.ToString());
        }).PoolInTime(1);
    }
    void Update()
    {
        EventDispatcher.TriggerEvent("PoolInTime", (Vector2)Input.mousePosition);
        EventDispatcher.TriggerEvent("PoolInTime", (Vector2)Input.mousePosition);
        EventDispatcher.TriggerEvent("PoolInTime", (Vector2)Input.mousePosition);
        EventDispatcher.TriggerEvent("PoolInTime", (Vector2)Input.mousePosition);
        EventDispatcher.TriggerEvent("PoolInTime", (Vector2)Input.mousePosition);
    }
    
}
