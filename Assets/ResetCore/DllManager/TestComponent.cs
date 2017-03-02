using ResetCore.ReAssembly;
using UnityEngine;
using ResetCore.Event;

public class TestComponent : MonoBehaviour {

    [InjectProperty("KVOInjector")]
    public Vector3 testProp { get; set; }

    [InjectProperty("KVOInjector")]
    public int testInt { get; set; }
    // Use this for initialization
    void Start () {
        
	}
	
}
