using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using ResetCore.Util;
using System.Xml.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ResetCore.Asset
{
    

    public class VersionManager : Singleton<VersionManager>
    {
        public static readonly string versionFilePath = Path.Combine(PathConfig.VersionDataResourcesPath, PathConfig.VersionDataPathInResources) + VersionData.Ex;

        private VersionData _versionData;
        public VersionData versionData
        {
            get
            {
                if (_versionData == null)
                {
#if UNITY_EDITOR
                    if (!File.Exists(versionFilePath))
                    {
                        _versionData = ScriptableObject.CreateInstance<VersionData>();
                        _versionData.appVersion = new Version(0, 0, 0, 1);
                        _versionData.resVersion = new Version(0, 0, 0, 1);
                        _versionData.MD5 = "";
                        Debug.Log(versionFilePath);
                        PathEx.MakeDirectoryExist(versionFilePath);
                        AssetDatabase.CreateAsset(_versionData, versionFilePath.Replace("\\", "/").Replace(PathConfig.projectPath + "/", ""));
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        _versionData = Resources.Load(PathConfig.VersionDataPathInResources) as VersionData;
                    }
#else
                    _versionData = Resources.Load(PathConfig.VersionDataPathInResources) as VersionData;
                    if (File.Exists(PathConfig.LocalVersionDataInPersistentDataPath))
                    {
                        _versionData.ParseXml(XDocument.Load(PathConfig.LocalVersionDataInPersistentDataPath));
                    }
#endif
                }
                return _versionData;
            }
            set
            {
                _versionData = value;
                EditorUtility.SetDirty(_versionData);
            }
        }

       

    }

}
