﻿using UnityEngine;
using System.Collections;

public class Driver : MonoBehaviour {

    private static Driver Instance;
    public static Driver instance
    {
        get { return Instance; }
    }


    void Awake()
    {
        Instance = this;
        Init();
    }

	// Use this for initialization
	void Start () {
        ResourcesLoaderHelper.instance.loader.LoadResource("heihei", null);
	}

    private void Init()
    {
        ProtoData<m.TestData> testData = new ProtoData<m.TestData>();
        Debug.Log(testData[1].id);
    }


}
