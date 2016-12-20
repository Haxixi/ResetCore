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


            menifestDoc.Save(PlatformConst.androidMenifestPath);


        }

        private static void HandleSdk(string sdkMenifestPath)
        {

        }

    }
}
