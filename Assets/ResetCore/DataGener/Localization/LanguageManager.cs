using UnityEngine;
using System.Collections;
using ResetCore.Util;
using System.Collections.Generic;

namespace ResetCore.Data
{
    public class LanguageManager : Singleton<LanguageManager>
    {

        private Dictionary<int, Dictionary<string, string>> allLanguageDict;
        public LanguageConst.LanguageType currentLanguage { get; private set; }

        public string this[string key]
        {
            get
            {
                if (allLanguageDict == null)
                {
                    allLanguageDict = GetLanguageDict();
                }
                return allLanguageDict[(int)currentLanguage][key];
            }
            private set
            {
                allLanguageDict[(int)currentLanguage][key] = value;
            }
        }

        //设置当前语言
        public void SetLanguageType(LanguageConst.LanguageType type)
        {
            currentLanguage = type;
            
        }

        private Dictionary<int, Dictionary<string, string>> GetLanguageDict()
        {
            var result = new Dictionary<int, Dictionary<string, string>>();
            if (!MyXMLParser.LoadIntMap(LanguageConst.languageXmlFileName, out result))
            {
                return result;
            }
            return result;
        }

       
    }

}
