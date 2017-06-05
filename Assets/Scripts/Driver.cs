using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    TestUserData data;
    TestUserData data2;

    void Awake()
    {
        data = new TestUserData();
        data.Load();
        Debug.Log(data.testInt);
        data.testInt = 111;
        data.Save();

        data2 = new TestUserData();
        data2.Load();
        Debug.Log(data2.testInt);
    }

    void Update()
    {
        
    }
    
}
