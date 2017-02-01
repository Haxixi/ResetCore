using ResetCore.ReAssembly;
using UnityEngine;

[InjectAttribute("TestInject")]
public class TestComponent : MonoBehaviour {

    [ResetCore.ReAssembly.Loadable]
    public int testInt;

    [ResetCore.ReAssembly.Loadable]
    public GameObject go;

    [ResetCore.ReAssembly.Loadable]
    public GameObject go1;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
