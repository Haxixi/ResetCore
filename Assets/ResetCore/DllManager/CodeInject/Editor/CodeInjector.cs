using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResetCore.Util;
using UnityEditor;
using UnityEditor.Callbacks;

namespace ResetCore.ReAssembly
{
    [InitializeOnLoad]
    public class CodeInjector
    {
        [MenuItem("DllInjector/Run")]
        private static void TestMidCodeInjectoring()
        {
            MidCodeInjectoring();
        }


        private static bool ifMidCodeInjector = true;

        [PostProcessScene]
        private static void MidCodeInjectoring()
        {
            if (ifMidCodeInjector) return;
            // Don't CodeInjector when in Editor and pressing Play
            if (Application.isPlaying || EditorApplication.isPlaying) return;

        }

    }
}
