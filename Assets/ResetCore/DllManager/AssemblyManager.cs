using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace ResetCore.ReAssembly
{
    public class AssemblyManager
    {

        /// <summary>
        /// 编辑器默认的程序集Assembly-CSharp.dll
        /// </summary>
        private static Assembly defaultCSharpAssembly;

        /// <summary>
        /// 程序集缓存
        /// </summary>
        public static Dictionary<string, Assembly> assemblyCache = new Dictionary<string, Assembly>();

        /// <summary>
        /// 获取编辑器默认的程序集Assembly-CSharp.dll
        /// </summary>
        public static Assembly DefaultCSharpAssembly
        {
            get
            {
                //如果已经找到，直接返回
                if (defaultCSharpAssembly != null)
                    return defaultCSharpAssembly;

                //从当前加载的程序包中寻找，如果找到，则直接记录并返回
                Assembly[] assems = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assem in assems)
                {
                    //所有本地代码都编译到Assembly-CSharp中
                    if (assem.GetName().Name == "Assembly-CSharp")
                    {
                        //保存到列表并返回
                        defaultCSharpAssembly = assem;
                        break;
                    }
                }
                return defaultCSharpAssembly;
            }
        }

    }

}
