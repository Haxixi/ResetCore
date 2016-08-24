using UnityEngine;
using System.Collections;
using UnityEditor;
using ResetCore.Data;
using System;
using ResetCore.Asset;
using ResetCore.Excel;
using System.IO;

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

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Excel"))
            {
                EditorUtility.OpenWithDefaultApp(PathConfig.LanguageDataExcelPath);
            }

            if (GUILayout.Button("Open Const"))
            {
                UnityEngine.Object obj = EditorResources.GetAsset<UnityEngine.Object>("LanguageConst", "ResetCore", "Localization") as UnityEngine.Object;
                AssetDatabase.OpenAsset(obj);
            }
            EditorGUILayout.EndHorizontal();

            if (!File.Exists(PathConfig.LanguageDataPath))
            {
                EditorGUILayout.HelpBox("You have not exported the localization file, export please!", MessageType.Error);
            }

            if (GUILayout.Button("Export Localization"))
            {
                Excel2Localization.ExportExcelFile();
            }

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
