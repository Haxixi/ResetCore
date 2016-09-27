using UnityEngine;
using System.Collections;
using System.Reflection;

namespace ResetCore.ReAssembly
{
    /// <summary>
    /// 用于从Assembly中加载出Component然后加到物体上面
    /// </summary>
    public class ScriptComponentLoader : MonoBehaviour
    {
        //assembly名称
        [SerializeField]
        private string assemblyName;
        
        //类型名称
        [SerializeField]
        private string componentName;

        /// <summary>
        /// 默认Assembly
        /// </summary>
        private static Assembly defaultCSharpAssembly;
        public static Assembly DefaultCSharpAssembly
        {
            get
            {
               return AssemblyManager.DefaultCSharpAssembly;
            }
        }

        void Awake()
        {
            Assembly assemble = DefaultCSharpAssembly;
            if(!string.IsNullOrEmpty(assemblyName))
            {
                assemble = AssemblyManager.GetAssembly(assemblyName);
            }
            System.Type type = assemble.GetType(componentName);
            if (type == null)
            {
                Debug.LogError("script " + componentName + " can not be found in " + (string.IsNullOrEmpty(assemblyName)? "defaultCSharpAssembly" : assemblyName));
                return;
            }
            if (componentName != null)
            {
                gameObject.AddComponent(type);
            }
            Destroy(this);
        }
    }

}
