using UnityEngine;
using System.Xml.Linq;
using System.Collections.Generic;
using System;

namespace ResetCore.PlatformHelper
{
    public class AndroidMenifest
    {

        XDocument xdoc;
        public bool valid { get; private set; }
        private AndroidMenifest() { }
        public static AndroidMenifest Load(string path)
        {
            AndroidMenifest menifest = new AndroidMenifest();
            try
            {
                menifest.xdoc = XDocument.Load(path);
            }catch(Exception e)
            {
                Debug.logger.LogError("PlatformHelper", e);
                Debug.logger.LogError("PlatformHelper", "加载失败");
                menifest.valid = false;
            }
            return menifest;
        }

        //uses-permission
        public List<string> usesPermissionList { get; private set; }
        //permission
        public List<string> permissionList { get; private set; }

        //最低版本号
        public int minSdk { get; private set; }

        //目标版本号
        public int maxSdk { get; private set; }


    }
}
