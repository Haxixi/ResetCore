using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using ResetCore.Util;
using ResetCore.Xml;

namespace ResetCore.PlatformHelper
{
    public static class AndroidPluginBuilder
    {
        /// <summary>
        /// 将Android Plugins回复到初始状态
        /// </summary>
        public static void RefreshAndroidPlugin()
        {
            Directory.Delete(PlatformConst.androidPath);
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
            XDocument menifestDoc = XDocument.Load(PlatformConst.androidMenifestPath);
            HandleBuildDoc(menifestDoc, buildFilePath);
            sdkMenifestPaths.ForEach((path) =>
            {
                HandleSdk(menifestDoc, path);
            });
        }

        private static void HandleBuildDoc(XDocument menifestDoc, string buildFilePath)
        {
            BuildFile buildFile = BuildFile.Load(buildFilePath);
            
        }

        private static void HandleSdk(XDocument menifestDoc, string sdkMenifestPath)
        {

        }

    }
}
