﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

using ResetCore.Asset;
using ResetCore.Util;
using System.IO;

namespace ResetCore.Xml
{
    public class XMLParser
    {
        //创建表
        public static bool LoadIntMap(string fileName, out Dictionary<int, Dictionary<string, string>> dicFromXml, string rootPath = null)
        {
            TextAsset textAsset = null;

            if(rootPath == null)
            {
                textAsset =
                    Resources.Load<TextAsset>(PathConfig.GetLocalGameDataResourcesPath(PathConfig.DataType.Xml) + fileName);
            }
            else
            {
                textAsset =
                    Resources.Load<TextAsset>(Path.Combine(rootPath,fileName).Replace("\\", "/"));
            }

            if (textAsset == null)
            {
                Debug.logger.LogError("XMLParser", fileName + " 文本加载失败");
            }
            XDocument xDoc = XDocument.Parse(textAsset.text);
            XElement root = xDoc.Root;
            dicFromXml = new Dictionary<int, Dictionary<string, string>>();
            if (xDoc == null) return false;
            int id = 1;
            //Debug.Log("Elements.Count" + root.Elements());
            foreach (XElement item in root.Elements())
            {
                Dictionary<string, string> propDic = new Dictionary<string, string>();
                foreach (XElement propItem in item.Elements())
                {
                    string key = propItem.Name.LocalName;
                    if (!propDic.ContainsKey(key))
                    {
                        propDic.Add(key, propItem.Value);
                    }
                    else
                    {
                        Debug.logger.LogError("XMLPraser", "已经拥有相同的键值" + key);
                    }

                }
                dicFromXml.Add(id, propDic);
                id++;
            }
            return true;
        }
        
        //创建Instance
        public static bool LoadInstance(string fileName, out Dictionary<string, string> dicFromXml, string rootPath = null)
        {
            TextAsset textAsset = null;
            Debug.Log(PathConfig.GetLocalGameDataResourcesPath(PathConfig.DataType.Pref) + fileName);
            if (rootPath == null)
            {
                textAsset =
                    Resources.Load<TextAsset>(PathConfig.GetLocalGameDataResourcesPath(PathConfig.DataType.Pref) + fileName);
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
            XDocument xDoc = XDocument.Parse(textAsset.text);
            XElement root = xDoc.Root;
            dicFromXml = new Dictionary<string, string>();

            if (!Alert.AlertIfNull(xDoc, "Cant Prase your xml!")) return false;

            foreach (XElement item in root.Elements())
            {
                string key = item.Name.LocalName;
                if (!dicFromXml.ContainsKey(key))
                {
                    dicFromXml.Add(key, item.Value);
                }
                else
                {
                    Debug.logger.LogError("XMLPraser", "已经拥有相同的键值" + key);
                }
            }
            return true;
        }

    }
}

