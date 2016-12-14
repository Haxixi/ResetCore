using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using ResetCore.Util;
using System.Xml.XPath;

namespace ResetCore.PlatformHelper
{
    public static class AndroidPluginBuilder
    {

        private static XDocument menifestDoc;

        /// <summary>
        /// 将Android Plugins回复到初始状态
        /// </summary>
        public static void RefreshAndroidPlugin()
        {
            Directory.Delete(PlatformConst.androidPath, true);
            DirectoryEx.DirectoryCopy(PlatformConst.androidBasePluginPath, PlatformConst.androidPath, true);
        }

        /// <summary>
        /// 将build文件中的属性与第三方SDK中Menifest的属性融合入原生Menifest中
        /// </summary>
        /// <param name="buildFilePath"></param>
        /// <param name="sdkMenifestPaths"></param>
        public static void BuildMenifest(string buildFilePath, List<string> sdkMenifestPaths)
        {
            RefreshAndroidPlugin();
            menifestDoc = XDocument.Load(PlatformConst.androidMenifestPath);
            HandleBuildDoc(buildFilePath);
            sdkMenifestPaths.ForEach((path) =>
            {
                HandleSdk(path);
            });
        }

        private static void HandleBuildDoc(string buildFilePath)
        {
            BuildFile buildFile = BuildFile.Load(buildFilePath);
            Debug.Log(menifestDoc);
            var manifestRoot = menifestDoc.XPathSelectElement("manifest");
            Debug.Log(manifestRoot);
            manifestRoot.SetAttributeValue("android:versionCode", buildFile.versionCode);
            manifestRoot.SetAttributeValue("android:versionName", buildFile.versionName);
            manifestRoot.SetAttributeValue("package", buildFile.packageName);

            //var metaDataEles = menifestDoc.XPathSelectElements("manifest/application/meta-data");
            //var applicationEle = menifestDoc.XPathSelectElement("manifest/application");

            //foreach (var kvp in buildFile.metaDatas)
            //{
            //    bool exist = false;
            //    foreach (var metaDataEle in metaDataEles)
            //    {
            //        if (metaDataEle.Attribute("android:name").Value == kvp.Key)
            //        {
            //            exist = true;
            //            break;
            //        }
            //    }
            //    if (!exist)
            //    {
            //        var metaDataEle = new XElement("meta-data");
            //        metaDataEle.SetAttributeValue("android:name", kvp.Key);
            //        metaDataEle.SetAttributeValue("android:value", kvp.Value);
            //        applicationEle.Add(metaDataEle);
            //    }
            //}


            menifestDoc.Save(PlatformConst.androidMenifestPath);


        }

        private static void HandleSdk(string sdkMenifestPath)
        {

        }

    }
}
