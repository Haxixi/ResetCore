using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    void Awake()
    {
        EventDispatcher.AddEventListener<Vector2>("DoubleClick", vec =>
        {
            //处理双击事件
        }).PoolByNum(2).ResetPoolByTime(1);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventDispatcher.TriggerEvent("DoubleClick", (Vector2)Input.mousePosition);
        }
    }
    
}
