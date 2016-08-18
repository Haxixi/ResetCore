using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ResetCore.Asset
{
    public class EditorResources
    {
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="fileNameFilter">文件名过滤器</param>
        /// <param name="pathFilter">路径过滤器</param>
        /// <returns></returns>
        public static T getAsset<T>(string fileNameFilter, params string[] pathFilter) where T : UnityEngine.Object
        {
            string[] guids = AssetDatabase.FindAssets(fileNameFilter);
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                int i = 0;
                for (; i < pathFilter.Length; i++)
                    if (!path.Contains(pathFilter[i])) break;
                if (i == pathFilter.Length)
                    return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
            }
            return null;
        }
    }

}
