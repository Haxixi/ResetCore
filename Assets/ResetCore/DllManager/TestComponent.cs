using ResetCore.ReAssembly;
using UnityEngine;
using ResetCore.Event;

public class TestComponent : MonoBehaviour {

    [InjectProperty("KVOInjector")]
    public int testProp { get; set; }

    [InjectProperty("KVOInjector")]
    public float testFloat { get; set;}

    // Use this for initialization
    void Start () {
        
	}
	
}
