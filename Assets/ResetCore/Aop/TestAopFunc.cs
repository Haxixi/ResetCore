using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Remoting.Messaging;

public class TestAopFunc : AopFunc {

    public override bool CanExecute(IMethodCallMessage msg)
    {
        return true;
    }

    public override object Execute(Func<object> act)
    {
        Debug.Log("BeforeTestAOP");
        object res = act();
        Debug.Log("AfterTestAOP");
        return res;
    }
}
