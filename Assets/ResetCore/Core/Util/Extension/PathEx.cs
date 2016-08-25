using UnityEngine;
using System.Collections;
using System.IO;

namespace ResetCore.Util
{
    public class PathEx
    {

        public static void MakeDirectoryExist(string path)
        {
            string root = Path.GetDirectoryName(path);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
        }

        public static string Combine(params string[] paths)
        {
            string result = "";
            foreach(string path in paths)
            {
                result = Path.Combine(result, path);
            }
            return result;
        }

        public static string GetPathParentFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(path);
        }
    }

}
