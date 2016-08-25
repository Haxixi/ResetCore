using UnityEngine;
using System.Collections;
using UnityEditor;
using ResetCore.Asset;
using System;
using ResetCore.Util;
using System.Collections.Generic;
using System.IO;
using ResetCore.Data.GameDatas.Xml;

namespace ResetCore.ModuleControl
{
    public class ModuleControlWindow : EditorWindow
    {
        private static Dictionary<MODULE_SYMBOL, bool> isImportDict;
        private SDKManager sdkManager = new SDKManager();

        private static bool inited = false;

        [MenuItem("Tools/ResetCore Module Controller")]
        static void ShowMainWindow()
        {
            ModuleControlWindow window =
                EditorWindow.GetWindow(typeof(ModuleControlWindow), false, "Module Controller") as ModuleControlWindow;
            window.Show();
        }

        private static void Init()
        {
            if (inited) return;
            isImportDict = new Dictionary<MODULE_SYMBOL, bool>();
            Array versionSymbols = Enum.GetValues(typeof(MODULE_SYMBOL));
            
            foreach (MODULE_SYMBOL symbol in versionSymbols)
            {
                if (!ModuleControl.isDevelopMode)
                {
                    isImportDict.Add(symbol, ModuleControl.ContainSymbol(symbol));
                }
                else
                {
                    isImportDict.Add(symbol, true);
                }
            }
            inited = true;
        }

        void OnGUI()
        {
            Init();
            ShowDeveloperMode();
            ShowSymbols();
            EditorGUILayout.Space();
            ShowFunctionButton();
            ShowWhenStart();
        }

        private void ShowDeveloperMode()
        {
            GUILayout.Label("Do you want to open develop mode", GUIHelper.MakeHeader(30));
            EditorGUILayout.Space();
            ModuleControl.isDevelopMode = EditorGUILayout.Toggle("Open Develop Mode", ModuleControl.isDevelopMode, GUILayout.Width(200));
        }

        bool ifShowWhenStart;
        private void ShowWhenStart()
        {
            ifShowWhenStart = PlayerPrefs.GetInt("ShowResetModuleController", 1) == 1;
            ifShowWhenStart = GUILayout.Toggle(ifShowWhenStart, "Show when start");
            if (ifShowWhenStart)
            {
                PlayerPrefs.SetInt("ShowResetModuleController", 1);
            }
            else
            {
                PlayerPrefs.SetInt("ShowResetModuleController", 0);
            }
        }

        private void ShowSymbols()
        {
            GUILayout.Label("Select the module you want to use", GUIHelper.MakeHeader(30));
            EditorGUILayout.Space();

            Array versionSymbols = Enum.GetValues(typeof(MODULE_SYMBOL));
            foreach (MODULE_SYMBOL symbol in versionSymbols)
            {
                EditorGUILayout.BeginHorizontal();
                string symbolName = ModuleConst.SymbolName[symbol];

                if (ModuleConst.defaultSymbol.Contains(symbol))
                {
                    GUILayout.Label("Core：" + symbolName, GUIHelper.MakeHeader(30), GUILayout.Width(200));
                }
                else
                {
                    if (!ModuleControl.isDevelopMode)
                    {
                        isImportDict[symbol] = EditorGUILayout.Toggle(symbolName, isImportDict[symbol], GUILayout.Width(200));
                    }
                    else
                    {
                        GUILayout.Label("Other：" + symbolName, GUIHelper.MakeHeader(30), GUILayout.Width(200));
                    }
                }
                ShowSDKSetup(symbol);

                GUILayout.Label(ModuleConst.SymbolComments[symbol]);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void ShowSDKSetup(MODULE_SYMBOL symbol)
        {
            if (!ModuleConst.NeedSDKDict.ContainsKey(symbol) || isImportDict[symbol] == false)
                return;

            SDKType sdkType = ModuleConst.NeedSDKDict[symbol];
            bool hasSetup = sdkManager.HasSetuped(sdkType);
            if (hasSetup)
            {
                GUILayout.Label("SDK：" + sdkType.ToString() +
                    " has setuped", GUIHelper.MakeHeader(30), GUILayout.Width(200));
            }
            else
            {
                if (!ModuleControl.isDevelopMode)
                {
                    if (GUILayout.Button("Need Setup", GUILayout.Width(100)))
                    {
                        if (EditorUtility.DisplayDialog("Setup SDK",
                            "Do you want to setup " + sdkType.ToString() + " ?"
                            , "OK", "NO"))
                        {
                            sdkManager.SetupSDK(sdkType);

                        }
                    }
                }
                else
                {
                    GUILayout.Label("SDK：" + sdkType.ToString() +
                        " no setuped", GUIHelper.MakeHeader(30), GUILayout.Width(200));
                }
               
            }
        }

        private void ShowFunctionButton()
        {
            if (!ModuleControl.isDevelopMode)
            {
                if (GUILayout.Button("Apply", GUILayout.Width(200)))
                {
                    ModuleControl.ApplySymbol(isImportDict);
                    inited = false;
                }
                if (GUILayout.Button("Refresh Backup", GUILayout.Width(200)))
                {
                    ModuleControl.RefreshBackUp();
                    inited = false;
                }
                if (GUILayout.Button("Remove ResetCore", GUILayout.Width(200)))
                {
                    ModuleControl.RemoveResetCore();
                    inited = false;
                }
            }
           
        }

       
    }

  
}
