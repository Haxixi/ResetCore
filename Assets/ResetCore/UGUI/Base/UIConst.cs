using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace ResetCore.UGUI
{
    public static class UIConst
    {
        //存储Prefab的路径
        public static string uiPrefabPath = "UI";

        public enum UIName
        {
            TestUI,
            HAHAUI
        }

        public static Dictionary<UIName, string> UIPrefabNameDic = new Dictionary<UIName, string>
        {
            {UIName.TestUI, "TestUI.prefab" },
            {UIName.HAHAUI, "HAHAUI.prefab" }
        };

    }
}

