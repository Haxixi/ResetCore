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
