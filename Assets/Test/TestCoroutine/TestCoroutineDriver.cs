using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResetCore.Util;

public class TestCoroutineDriver : MonoBehaviour {

    private void Awake()
    {
        ReCoroutinesManager.Instance.AddCoroutine(TestCoroutine());
        StartCoroutine(TestCoroutine2());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator TestCoroutine()
    {
        Debug.Log("haha");
        yield return 3;
        Debug.Log("asdasdasd");
    }

    IEnumerator TestCoroutine2()
    {
        Debug.Log("haha");
        yield return new WaitForSeconds(3);
        Debug.Log("asdasdasd");
    }
}
