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
        EventDispatcher.AddEventListener<int, int, int, int>("Test", Test);
        EventDispatcher.TriggerEvent("Test", 1, 2, 3, 4);
        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            EventDispatcher.RemoveEventListener<int, int, int, int>("Test", Test);
            EventDispatcher.TriggerEvent("Test", 1, 2, 3, 4);
            Debug.Log("1111111111111111111");
        }, 2);
    }

    public void Test(int i1, int i2, int i3, int i4)
    {
        Debug.Log(i1);
        Debug.Log(i2);
        Debug.Log(i3);
        Debug.Log(i4);
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
