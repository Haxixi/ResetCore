using UnityEngine;
using System.Collections;

namespace ResetCore.NetPost
{
    /// <summary>
    /// 用于APC传输
    /// </summary>
    [System.Serializable]
    public class APCBean
    {
        /// <summary>
        /// 函数名
        /// </summary>
        public string functionName;
        /// <summary>
        /// 参数名
        /// </summary>
        public ArrayList argList;

        public APCBean(string functionName, ArrayList argList)
        {
            this.functionName = functionName;
            this.argList = argList;
        }
    }
}
