using UnityEngine;
using System.Collections;

namespace ResetCore.UGUI
{
    public static class UGUIUtil
    {
        /// <summary>
        /// 是否为UI组件
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool IsUIComponent(this Component component)
        {
            return component.GetType().Assembly.GetName().Name != "UnityEngine.UI";
        }
    }

}
