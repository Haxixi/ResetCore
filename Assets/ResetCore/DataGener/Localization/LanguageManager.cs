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

        public static LanguageConst.LanguageType currentLanguage = LanguageConst.defaultLanguage;
        private static Dictionary<int, Dictionary<string, string>> _allLanguageDict;
        private static Dictionary<int, Dictionary<string, string>> allLanguageDict
        {
            get
            {
                if(_allLanguageDict == null)
                {
                    _allLanguageDict = instance.GetLanguageDict();
                }
                return _allLanguageDict;
            }
        }

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
            
            if (!ContainWord(key, type))
            {
                return string.Empty;
            }

            return allLanguageDict[(int)type][key];
        }

        /// <summary>
        /// 是否存在该键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainWord(string key)
        {
            return ContainWord(key, currentLanguage);
        }
        public static bool ContainWord(string key, LanguageConst.LanguageType type)
        {
            if (!allLanguageDict.ContainsKey((int)type))
                return false;

            if (!allLanguageDict[(int)type].ContainsKey(key))
            {
                return false;
            }

            return true;
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
