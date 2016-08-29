using UnityEngine;
using System.Collections;
using System.IO;

namespace ResetCore.Util
{
    public class PathEx
    {
        /// <summary>
        /// 使目录存在,Path可以是目录名也可以是文件名
        /// </summary>
        /// <param name="path"></param>
        public static void MakeDirectoryExist(string path)
        {
            string root = Path.GetDirectoryName(path);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
        }
        /// <summary>
        /// 结合目录
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            string result = "";
            foreach(string path in paths)
            {
                result = Path.Combine(result, path);
            }
            MakePathStandard(result);
            return result;
        }

        /// <summary>
        /// 获取父文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPathParentFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(path);
        }
        
        /// <summary>
        /// 将绝对路径转换为相对于Asset的路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertAbstractToAssetPath(string path)
        {
            return MakePathStandard(path.Replace(PathConfig.projectPath + "/", ""));
        }

        /// <summary>
        /// 使路径标准化，去除空格并将所有'\'转换为'/'
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MakePathStandard(string path)
        {
            return path.Trim().Replace("\\", "/");
        }
    }

}
