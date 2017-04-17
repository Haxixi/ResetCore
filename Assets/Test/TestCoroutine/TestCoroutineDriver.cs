using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResetCore.Util;
using MovementEffects;

public class TestCoroutineDriver : MonoBehaviour {

    private void Awake()
    {
        for (int i = 0; i < 100000; i++)
        {
            //ReCoroutinesManager.AddCoroutine(TestCoroutine());
            StartCoroutine(TestCoroutine2());
            //Timing.RunCoroutine(TestCoroutine3());
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    IEnumerator<float> TestCoroutine()
    {
        int sum = 0;
        for (int i = 0; i < int.MaxValue; i++)
        {
            sum++;
            yield return 0;
        }
    }

    IEnumerator<float> TestWaitCor()
    {
        Debug.Log("BeginWait");
        WWW www = new WWW("http://121.196.216.106:4040/Escape/ServerAddress.txt");
        yield return ReCoroutine.WaitWWW(www);
        Debug.Log(www.text);
        Debug.Log("EndWait");
    }

    IEnumerator TestCoroutine2()
    {
        int sum = 0;
        for (int i = 0; i < int.MaxValue; i++)
        {
            sum++;
            yield return null;
        }
    }

    IEnumerator<float> TestCoroutine3()
    {
        int sum = 0;
        for (int i = 0; i < int.MaxValue; i++)
        {
            sum++;
            yield return 0;
        }
    }
}
