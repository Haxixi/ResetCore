using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResetCore.Util;
using System;

public class TestPass : BaseAysnPass<List<string>, string>
{

    public override void HandlePass(List<string> input, Action<object> outputHandler)
    {
        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            string temp = "";
            foreach (var i in input)
            {
                temp += i;
            }
            outputHandler(temp);
        }, 1);
    }
}

public class TestIntPass : BaseAysnPass<string, int>
{
    public override void HandlePass(string input, Action<object> outputHandler)
    {
        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            outputHandler(int.Parse(input));
        }, 1);
    }
}
