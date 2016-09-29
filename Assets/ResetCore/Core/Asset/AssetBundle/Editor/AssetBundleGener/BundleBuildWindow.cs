using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ResetCore.Util;
using System.Xml.Linq;
using System.Xml;
using ResetCore.Xml;

namespace ResetCore.Asset
{


    public class BundleBuildWindow : EditorWindow
    {

        //显示窗口的函数
        [MenuItem("Tools/Assets/AssetBundle Gener")]
        static void ShowMainWindow()
        {
            BundleBuildWindow window =
                EditorWindow.GetWindow(typeof(BundleBuildWindow), false, "AssetBundle压缩包生成器") as BundleBuildWindow;
            window.Show();
        }
        //路径列表
        private static List<string> pathList;
        private static List<string> bundlePathList;
        //是否已经导出Bundle
        private static bool hasExported
        {
            get { return EditorPrefs.HasKey(EditorPrefNames.AssetBundle.HasExported) ? EditorPrefs.GetBool(EditorPrefNames.AssetBundle.HasExported) : false; }
            set { EditorPrefs.SetBool(EditorPrefNames.AssetBundle.HasExported, value); }
        }
        //是否已经打包
        private static bool hasPackaged
        {
            get { return Directory.Exists(PathConfig.GetExportPathByVersion(resVersion)); }
        }
        //导出文件是否完整
        private static bool isAllFile = true;
        //版本号
        private static Version resVersion
        {
            get
            {
                return VersionManager.instance.versionData.resVersion;
            }
            set
            {
                VersionManager.instance.versionData.resVersion = value;
            }
        }

        private static Version appVersion
        {
            get
            {
                return VersionManager.instance.versionData.appVersion;
            }
            set
            {
                VersionManager.instance.versionData.appVersion = value;
            }
        }

        private void Init()
        {
            if (EditorPrefs.HasKey(EditorPrefNames.AssetBundle.Paths))
            {
                pathList = StringEx.GetValue(EditorPrefs.GetString(EditorPrefNames.AssetBundle.Paths), typeof(List<string>)) as List<string>;
            }
            else
            {
                pathList = new List<string>();
            }
        }

        void OnGUI()
        {
            Init();
            ShowPublicTools();
            EditorGUILayout.Space();
            ShowBundleTools();
            EditorGUILayout.Space();
            ShowPackageTools();
            EditorGUILayout.Space();
            ShowExportTools();
        }

        #region 显示基本工具
        //
        private void ShowPublicTools()
        {
            GUILayout.Label("文件添加工具", GUIHelper.MakeHeader(30));
            GUILayout.Label("当前文件数量为" + pathList.Count);
            if (GUILayout.Button("更新本地资源列表", GUILayout.Width(200)))
            {
                ResourcesListGen.UpdateResourcesList();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("添加文件", GUILayout.Width(200)))
            {
                AddFile();
                hasExported = false;
            }

            if (GUILayout.Button("清空文件", GUILayout.Width(200)))
            {
                ClearFile();
                hasExported = false;
            }
            if (GUILayout.Button("显示文件目录", GUILayout.Width(200)))
            {
                ShowFiles();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void AddFile()
        {
            string assetBundlePaths = "";
            var selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            var paths = (from s in selection
                         let path = AssetDatabase.GetAssetPath(s)
                         where File.Exists(path)
                         select path).ToArray();
            foreach (string path in paths)
            {
                if (!path.HasChinese() && !path.HasSpace())
                {
                    if (!pathList.Contains(path))
                    {
                        pathList.Add(path);
                        assetBundlePaths += "," + path;
                        Debug.logger.Log("AssetBundle压缩器", path + "添加成功");
                    }
                    else
                    {
                        Debug.logger.Log("AssetBundle压缩器", path + "已经存在");
                    }

                }
                else
                {
                    Debug.logger.LogError("AssetBundle压缩器", path + "不符合命名规范");
                }
            }
            assetBundlePaths.ReplaceFirst(",", "");
            EditorPrefs.SetString(EditorPrefNames.AssetBundle.Paths, assetBundlePaths);

        }

        private void ClearFile()
        {
            pathList.Clear();
            pathList = new List<string>();
            EditorPrefs.SetString(EditorPrefNames.AssetBundle.Paths, "");
            Debug.logger.Log("AssetBundle压缩器", "文件列表已经清空");
        }

        private void ShowFiles()
        {
            foreach (string path in pathList)
            {
                Debug.logger.Log("文件目录" + path);
            }
        }
        #endregion

        #region 打包工具
        private void ShowBundleTools()
        {
            GUILayout.Label("打包工具", GUIHelper.MakeHeader(30));
            GUILayout.Label("当前导出状态：" + (hasExported ? "已经导出" : "仍未导出"));
            CheckExportBundle();
            if (GUILayout.Button("导出Bundle", GUILayout.Width(200)))
            {
                GenAssetBundle();
                hasExported = true;
                isAllFile = true;
            }
        }

        private static void CheckExportBundle()
        {

            if (hasExported)
            {
                foreach (string path in pathList)
                {
                    string fileName = FileEx.GetFileName(path);
                    string resPath = ResourcesLoaderHelper.GetResourcesBundlePathByObjectName(fileName);
                    if(resPath == null)
                    {
                        isAllFile = false;
                        Debug.logger.LogError("打包工具", "Resources文件缺失！" + path);
                    }
                    if (!File.Exists(resPath))
                    {
                        isAllFile = false;
                        Debug.logger.LogError("打包工具", "Bundle文件缺失！" + resPath);
                    }
                    resPath = ResourcesLoaderHelper.GetResourcesBundleManifestPathByObjectName(fileName);
                    if (!File.Exists(resPath))
                    {
                        isAllFile = false;
                        Debug.logger.LogError("打包工具", "Bundle文件缺失！" + resPath);
                    }
                }
            }
            if (!isAllFile)
            {
                hasExported = false;
                GUILayout.Label("发现文件缺失！请重新打包！");
            }
        }

        private static void GenAssetBundle()
        {
            ResourcesListGen.UpdateResourcesList();
            AssetBundleGen.CreateTargetFolder();
            AssetBundleGen.ExportBundle(pathList.ToArray(), PathConfig.bundleRootPath, true);
        }


        #endregion

        #region 压缩工具
        private void ShowPackageTools()
        {
            GUILayout.Label("Bundle打包工具", GUIHelper.MakeHeader(30));
            GUILayout.Label("当前打包状态：" + (hasPackaged ? "已经打包" : "仍未打包"));
            GUILayout.BeginHorizontal();

            GUILayout.Label("资源版本号");
            resVersion = Version.GetValue(GUILayout.TextField(resVersion.ToString()));
            if (GUILayout.Button("升级版本", GUILayout.Width(200)))
            {
                resVersion = new Version(resVersion.x, resVersion.y, resVersion.z, resVersion.w + 1);
            }

            GUILayout.Label("应用版本号");
            appVersion = Version.GetValue(GUILayout.TextField(appVersion.ToString()));
            if (GUILayout.Button("升级版本", GUILayout.Width(200)))
            {
                appVersion = new Version(appVersion.x, appVersion.y, appVersion.z, appVersion.w + 1);
            }

            GUILayout.EndHorizontal();
            if (GUILayout.Button("打包", GUILayout.Width(200)))
            {
                if (hasExported && isAllFile)
                {
                    CompressPackage();
                }
                else
                {
                    Debug.logger.LogError("压缩失败", "请先进行打包");
                }
            }
        }

        private static void CompressPackage()
        {
            bundlePathList = new List<string>();
            string mainBundlePath = PathConfig.AssetRootBundleFilePath;
            bundlePathList.Add(mainBundlePath);
            bundlePathList.Add(mainBundlePath + AssetBundleConst.ManifestEx);
            foreach (string path in pathList)
            {
                string fileName = FileEx.GetFileName(path);
                string resPath = ResourcesLoaderHelper.GetResourcesBundlePathByObjectName(fileName);
                bundlePathList.Add(resPath);
                resPath = ResourcesLoaderHelper.GetResourcesBundleManifestPathByObjectName(fileName);
                bundlePathList.Add(resPath);
            }
            foreach (string path in bundlePathList)
            {
                Debug.logger.Log("压缩", path + "压缩");
            }
            CompressHelper.CompressFiles(PathConfig.bundleRootPath, bundlePathList.ToArray(), PathConfig.GetPkgExportPath(resVersion));
            string MD5 = MD5Utils.BuildFileMd5(PathConfig.GetPkgExportPath(resVersion));
            GenVersionInfo(MD5);
        }

        private static void GenVersionInfo(string MD5)
        {
            //TODO　设置应用版本
            VersionManager.instance.versionData.appVersion = appVersion;
            VersionManager.instance.versionData.resVersion = resVersion;
            VersionManager.instance.versionData.MD5 = MD5;
            AssetDatabase.SaveAssets();
            string versionDataFilePath = PathConfig.GetExportPathByVersion(resVersion) + PathConfig.VersionDataName + ".xml";
            VersionManager.instance.versionData.GenXml(versionDataFilePath);
        }

        #endregion

        #region 导出工具
        private void ShowExportTools()
        {
            GUILayout.Label("文件导出工具");
            if (GUILayout.Button("将文件导出到本地服务器", GUILayout.Width(200)))
            {
                string localBundleResourcesFolder = PathConfig.GetLocalBundleResourcesFolderByVersion(resVersion);
                if (Directory.Exists(localBundleResourcesFolder))
                {
                    Directory.Delete(localBundleResourcesFolder, true);
                }
                DirectoryEx.DirectoryCopy(PathConfig.GetExportPathByVersion(resVersion), localBundleResourcesFolder, true);


                string[] versionResFolderList = Directory.GetDirectories(PathConfig.localUpdateBundleUrl);
                XDocument xDoc = new XDocument();
                XElement root = new XElement("Root");
                xDoc.Add(root);
                foreach (string path in versionResFolderList)
                {
                    string versionFolder = Path.GetFileName(path.Replace("\\", "/"));
                    root.Add(new XElement("version", versionFolder));
                }
                xDoc.SafeSaveWithoutDeclaration(PathConfig.localVersionInfoUrl);
               
            }
        }
        #endregion
    }

}