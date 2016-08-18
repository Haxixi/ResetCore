using UnityEngine;
using System.Collections;
using UnityEditor;
using ResetCore.Asset;

namespace ResetCore.Util
{
    public class UGUITools : MonoBehaviour
    {
        [MenuItem("Tools/UGUI/Create UIManager")]
        public static void CreateUIManager()
        {
            Object obj = EditorResources.GetAsset<Object>("UIManager", "ResetCore", "Resources");
            GameObject go = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
            go.name = "UIManager";
        }

       
    }
}

