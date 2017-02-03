using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResetCore.Util;
using UnityEditor;
using UnityEditor.Callbacks;

namespace ResetCore.ReAssembly
{
    public class CodeInjector
    {
        #region 打包注入
        //是否已经生成
        private static bool hasGen = false;
        [PostProcessBuild(1000)]
        private static void OnPostprocessBuildPlayer(BuildTarget buildTarget, string buildPath)
        {
            hasGen = false;
        }

        [PostProcessScene]
        public static void TestInjectMothodOnPost()
        {
            if (hasGen == true) return;
            hasGen = true;

            TestInjectMothod();
        }
        #endregion

        #region 编辑器下注入
        [InitializeOnLoadMethod]
        public static void TestInjectMothod()
        {
            if (Application.isPlaying)
                return;
            CodeInjectorSetting setting = new CodeInjectorSetting();
            setting.RunInject();
        }
        #endregion
    }
}
