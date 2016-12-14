using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using ResetCore.Xml;
using System;

namespace ResetCore.PlatformHelper
{
    public sealed class BuildFile
    {
        private BuildFile() { }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="buildFilePath"></param>
        /// <returns></returns>
        public static BuildFile Load(string buildFilePath)
        {
            BuildFile res = new BuildFile();
            Type fileType = typeof(BuildFile);
            XDocument buildFile = XDocument.Load(buildFilePath);
            foreach (var property in fileType.GetProperties())
            {
                XElement ele = buildFile.Root.Element(property.Name);
                if (ele == null)
                    continue;

                object value = ele.ReadValueFromElement(property.PropertyType);
                property.SetValue(res, value, new object[0]);
            }
            return res;
        }

        /// <summary>
        ///  包名
        /// </summary>
        public string packageName { get; private set; }

        /// <summary>
        /// 最小SDK版本号
        /// </summary>
        public int minSdkVersion { get; private set; }

        /// <summary>
        /// 目标SDK版本号
        /// </summary>
        public int targetSdkVersion { get; private set; }

        /// <summary>
        /// App名称
        /// </summary>
        public string appName { get; private set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string versionCode { get; private set; }

        /// <summary>
        /// 版本名
        /// </summary>
        public string versionName { get; private set; }

        /// <summary>
        /// 配置数据
        /// </summary>
        public Dictionary<string, string> metaDatas { get; private set; }
    }

}
