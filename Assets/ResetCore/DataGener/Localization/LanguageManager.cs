using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;
using System.IO;

namespace ResetCore.Data
{
    public class LanguageManager : Singleton<LanguageManager>
    {

        private Dictionary<int, Dictionary<string, string>> allLanguageDict;
        public LanguageConst.LanguageType currentLanguage { get; private set; }

        public override void Init()
        {
 	        base.Init();
            currentLanguage = LanguageConst.LanguageType.Chinese;

        }
        /// <summary>
        /// 获取单词
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetWord(string key)
        {
            if (instance.allLanguageDict == null)
            {
                instance.allLanguageDict = instance.GetLanguageDict();
            }
            if (!instance.allLanguageDict[(int)instance.currentLanguage].ContainsKey(key))
            {
                Debug.logger.LogError("GetWord", "Not Exists " + key);
                return key;
            }
            return instance.allLanguageDict[(int)instance.currentLanguage][key];
        }

        /// <summary>
        /// 设置当前语言
        /// </summary>
        /// <param name="type"></param>
        public static void SetLanguageType(LanguageConst.LanguageType type)
        {
            instance.currentLanguage = type;
        }

        private Dictionary<int, Dictionary<string, string>> GetLanguageDict()
        {
            var result = new Dictionary<int, Dictionary<string, string>>();
            if (!MyXMLParser.LoadIntMap(Path.GetFileName(PathConfig.LanguageDataPath), out result))
            {
                return result;
            }
            return result;
        }

        
       
    }

}
