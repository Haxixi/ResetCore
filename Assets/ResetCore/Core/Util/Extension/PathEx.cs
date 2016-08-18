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
    }

}
