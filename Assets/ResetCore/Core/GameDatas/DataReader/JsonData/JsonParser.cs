using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

namespace ResetCore.Json
{
    public class JsonParser
    {
        //加载Json
        public static bool LoadIntMap(string fileName,
            out Dictionary<int, Dictionary<string, string>> dicFromXml, string rootPath = null)
        {
            TextAsset textAsset = null;
            dicFromXml = new Dictionary<int, Dictionary<string, string>>();

            if (rootPath == null)
            {
                textAsset =
                    Resources.Load<TextAsset>(PathConfig.GetLocalGameDataResourcesPath(PathConfig.DataType.Json) + fileName);
            }
            else
            {
                textAsset =
                    Resources.Load<TextAsset>(Path.Combine(rootPath, fileName).Replace("\\", "/"));
            }

            if (textAsset == null)
            {
                Debug.logger.LogError("XMLParser", fileName + " 文本加载失败");
            }

            JsonData data = JsonMapper.ToObject(textAsset.text);
            List<Dictionary<string, string>> strList = JsonMapper.ToObject<List<Dictionary<string, string>>>(data[fileName].ToJson());
            for(int i = 0; i < strList.Count; i++)
            {
                dicFromXml.Add(i + 1, strList[i]);
            }
            
            return true;
        }
    }

}
