
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxCollider))]
public class MyTest : DecoratorEditor
{
    public MyTest() : base("BoxColliderEditor") { }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Adding this button"))
        {
            Debug.Log("Adding this button");
        }
    }
}