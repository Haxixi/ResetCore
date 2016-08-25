using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ResetCore.Asset
{
    public class VersionData : ScriptableObject
    {
        //资源版本号
        public Version resVersion = new Version(0, 0, 0, 0);
        //应用版本号
        public Version appVersion = new Version(0, 0, 0, 0);
        //MD5列表
        public List<string> MD5List = new List<string>();
    }

}
