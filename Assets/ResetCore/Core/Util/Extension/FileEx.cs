using UnityEngine;
using System.Collections;
using System.IO;

namespace ResetCore.Util
{
    public class FileEx
    {

        public static void SaveText(string text, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);
            sr.Write(text);//开始写入值
            sr.Close();
            fs.Close();
        }
    }

}
