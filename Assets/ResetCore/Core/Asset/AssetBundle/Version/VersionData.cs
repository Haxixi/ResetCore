using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using ResetCore.Util;

namespace ResetCore.Asset
{
    public class VersionData : ScriptableObject
    {

        public static readonly string Ex = ".asset";
        //资源版本号
        public Version resVersion = new Version(0, 0, 0, 0);
        //应用版本号
        public Version appVersion = new Version(0, 0, 0, 0);
        //MD5列表
        public string MD5 = "";

        public void GenXml(string outputPath)
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("Root");
            xDoc.Add(root);
            root.Add(new XElement("ResVersion", resVersion.ToString()));
            root.Add(new XElement("AppVersion", appVersion.ToString()));
            root.Add(new XElement("MD5", MD5));
            PathEx.MakeDirectoryExist(outputPath);
            xDoc.Save(outputPath);
        }

        public void ParseXml(XDocument xDoc)
        {
            resVersion = Version.GetValue(xDoc.Root.Element("ResVersion").Value);
            resVersion = Version.GetValue(xDoc.Root.Element("AppVersion").Value);
            MD5 = xDoc.Root.Element("MD5").Value;
        }
    }
}
