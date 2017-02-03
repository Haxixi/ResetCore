using ResetCore.ReAssembly;
using UnityEngine;
using ResetCore.Event;

[InjectAttribute("TestInject")]
public class TestComponent : MonoBehaviour {

    private int testProp { get; set; }

    private void Awake()
    {
        testProp = 10;
    }

    // Use this for initialization
    void Start () {
        Debug.Log(testProp);
	}
	
}
