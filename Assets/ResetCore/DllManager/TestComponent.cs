using ResetCore.ReAssembly;
using UnityEngine;
using ResetCore.Event;

public class TestComponent : MonoBehaviour {

    [InjectProperty("KVOInjector")]
    public int testProp { get; set; }

    [InjectProperty("KVOInjector")]
    public float testFloat { get; set;}

    private void Awake()
    {
        testProp = 10;
    }

    // Use this for initialization
    void Start () {
        
	}
	
}
