using ResetCore.Util;
using System.Collections.Generic;
using ResetCore.NetPost;
using UnityEngine;
using ResetCore.Event;

//using ResetCore.Data.GameDatas;

public class Driver : MonoSingleton<Driver> {

    Pipeline<List<string>, int> pipeLine = new Pipeline<List<string>, int>();

    ActionQueueWithArg queue = new ActionQueueWithArg();

    void Awake()
    {
        pipeLine.AddPass(new TestPass())
            .AddPass(new TestIntPass())
            .AsynProcess(new List<string> { "1", "222" }, (res)=> {
            Debug.LogError(res);
        });
        

        //queue.AddAction((obj, act) =>
        //{
        //    string arg1 = obj as string;
        //    CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        //    {
        //        act(arg1 + "2222");
        //    }, 1);
        //}).AddAction((obj, act)=> {
        //    string arg1 = obj as string;
        //    CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        //    {
        //        act(arg1 + "3333");
        //    }, 1);
        //}).AddAction((obj, act)=> {
        //    string arg1 = obj as string;
        //    Debug.Log(arg1);
        //});

        //queue.Start("1111");
    }

    void Update()
    {
        
    }
    
}
