using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using ResetCore.Util;
using ResetCore.Xml;

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
            xDoc.SafeSaveWithoutDeclaration(outputPath);
        }

        public static VersionData ParseXml(XDocument xDoc)
        {
            VersionData data = ScriptableObject.CreateInstance<VersionData>();
            data.resVersion = Version.GetValue(xDoc.Root.Element("ResVersion").Value);
            data.appVersion = Version.GetValue(xDoc.Root.Element("AppVersion").Value);
            data.MD5 = xDoc.Root.Element("MD5").Value;
            return data;
        }
    }
}
