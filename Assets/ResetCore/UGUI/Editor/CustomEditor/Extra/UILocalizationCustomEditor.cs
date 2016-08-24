using UnityEngine;
using System.Collections;
using UnityEditor;
using ResetCore.Data;
using System;

namespace ResetCore.UGUI
{
    [CustomEditor(typeof(UILocalization), false)]
    public class UILocalizationCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UILocalization local = target as UILocalization;
            //base.OnInspectorGUI();
            local.key = EditorGUILayout.TextField("Key", local.key);

            Array types = Enum.GetValues(typeof(LanguageConst.LanguageType));
            foreach(LanguageConst.LanguageType type in types)
            {
                GUILayout.Label(type.ToString());
                string helpTxt = LanguageManager.GetWord(local.key, type);
                EditorGUILayout.HelpBox(helpTxt, MessageType.None);
            }
        }

    }

}
