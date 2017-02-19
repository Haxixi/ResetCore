using UnityEngine;
using UnityEditor;
using ResetCore.PlatformHelper;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class TestBton {

	[MenuItem("Test/Test")]
	public static void EditorTest()
	{
        Action<Action> testTime = (act) =>
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            act();
            watch.Stop();
            UnityEngine.Debug.Log(watch.ElapsedTicks);
            watch.Reset();
        };

        Dictionary<int, int> testDict = new Dictionary<int, int>();
        Dictionary<string, int> testDict2 = new Dictionary<string, int>();

        for (int i = 0; i < 100000; i++)
        {
            testDict.Add(i, i + 1);
            testDict2.Add(i.ToString(), i + 1);
        }

        testTime(() =>
        {
            var item = testDict2["5000"];
        });

        //testTime(() =>
        //{
        //    var item = testDict2["50"];
        //});

    }
}
