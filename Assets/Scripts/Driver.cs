using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {


    void Awake()
    {
        ReCoroutineTaskManager.Instance.WaitForAllCoroutine(() =>
        {
            Debug.Log("FinishAll");
        }, new ReCoroutineTaskManager.CoroutineTask[] {
            EventDispatcher.AddEventListener("Test", Test).GetListenLatestCommandCoroutine(),
            EventDispatcher.AddEventListener("Test2", Test2).GetListenLatestCommandCoroutine(),
            EventDispatcher.AddEventListener("Test3", Test3).GetListenLatestCommandCoroutine(),
            EventDispatcher.AddEventListener("Test4", Test4).GetListenLatestCommandCoroutine()
        });

        ReCoroutineManager.AddCoroutine(TestCount());
    }

    IEnumerator<float> TestCount()
    {
        yield return 1;
        EventDispatcher.TriggerEvent("Test");
        yield return 1;
        EventDispatcher.TriggerEvent("Test2");
        yield return 1;
        EventDispatcher.TriggerEvent("Test3");
        yield return 1;
        EventDispatcher.TriggerEvent("Test4");
    }

    private void Test()
    {
        Debug.Log("Finish Test1");
    }

    private void Test2()
    {
        Debug.Log("Finish Test2");
    }

    private void Test3()
    {
        Debug.Log("Finish Test3");
    }

    private void Test4()
    {
        Debug.Log("Finish Test4");
    }

    void Update()
    {
        
    }
    
}
