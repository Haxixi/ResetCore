using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml.XPath;

namespace ResetCore.PlatformHelper
{
    public class AndroidMenifest
    {
        private static readonly Dictionary<string, string> propertyPath = new Dictionary<string, string>()
        {
            
        };

        XDocument xdoc;
        private AndroidMenifest() { }
        public static AndroidMenifest Load(string path)
        {
            AndroidMenifest menifest = new AndroidMenifest();
            menifest.xdoc = XDocument.Load(path);
            return menifest;
        }

        //uses-permission
        public List<string> usesPermissionList { get; private set; }
        //permission
        public List<string> permissionList { get; private set; }

        //
        public int minSdk { get; private set; }

        public int maxSdk { get; private set; }


    }
}
