using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;
using System.IO;
using ResetCore.Xml;
using ResetCore.Event;

namespace ResetCore.Data
{
    public class LanguageManager : Singleton<LanguageManager>
    {

        private static Dictionary<int, Dictionary<string, string>> allLanguageDict;
        public static LanguageConst.LanguageType currentLanguage = LanguageConst.LanguageType.Chinese;

        /// <summary>
        /// 获取单词
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetWord(string key)
        {
            return GetWord(key, currentLanguage);
        }

        public static string GetWord(string key, LanguageConst.LanguageType type)
        {
            if (allLanguageDict == null)
            {
                allLanguageDict = instance.GetLanguageDict();
            }

            if (!allLanguageDict.ContainsKey((int)type))
                return string.Empty;

            if (!allLanguageDict[(int)type].ContainsKey(key))
            {
#if !UNITY_EDITOR
                Debug.logger.LogError("GetWord", "Not Exists " + key);
#endif
                return key;
            }
            return allLanguageDict[(int)type][key];
        }

        /// <summary>
        /// 设置当前语言
        /// </summary>
        /// <param name="type"></param>
        public static void SetLanguageType(LanguageConst.LanguageType type)
        {
            currentLanguage = type;
            EventDispatcher.TriggerEvent(InnerEvents.UGUIEvents.OnLocalize);
        }

        private Dictionary<int, Dictionary<string, string>> GetLanguageDict()
        {
            var result = new Dictionary<int, Dictionary<string, string>>();
            if (!XMLParser.LoadIntMap(Path.GetFileNameWithoutExtension(PathConfig.LanguageDataPath), 
                out result, PathConfig.GetLocalGameDataResourcesPath(PathConfig.DataType.Localization)))
            {
                return result;
            }
            return result;
        }


    }

}
