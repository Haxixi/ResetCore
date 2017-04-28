using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {


    void Awake()
    {
        ReCoroutineManager.AddCoroutine(TestCount());
    }

    IEnumerator<float> TestCount()
    {
        int sum = 0;
        yield return ReCoroutine.WaitThreadOperation(() =>
        {
            for (int i = 0; i < 100000; i++)
                sum++;
        });
        Debug.Log(sum);
    }

    void Update()
    {
        
    }
    
}
