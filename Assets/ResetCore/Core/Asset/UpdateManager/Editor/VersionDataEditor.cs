using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ResetCore.Asset
{
    [CustomEditor(typeof(VersionData))]
    public class VersionDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            VersionData data = target as VersionData;
            GUILayout.Label("当前资源版本：" + data.resVersion.ToString(), GUIHelper.MakeHeader(30));
            GUILayout.Label("当前应用版本：" + data.appVersion.ToString(), GUIHelper.MakeHeader(30));
            //TODO
        }

    }

}
