﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using ResetCore.Util;
using System.IO;


namespace ResetCore.Asset
{
    public class ResourcesLoaderHelper : MonoSingleton<ResourcesLoaderHelper>
    {
        private static Dictionary<string, string> ResourcesList;
        //键为物体名，值为相对于Resources路径
        public static Dictionary<string, string> resourcesList 
        {
            get
            {
                if (ResourcesList == null)
                {
                    ResourcesList = LoadResourcesListFile();
                }
                return ResourcesList;
            }
            private set
            {
                ResourcesList = value;
            } 
        }
        public static readonly string ExName = ".ab";

        //根AssetBundle文件
        private AssetBundle MainBundle;
        public AssetBundle mainBundle
        {
            get
            {
                if (MainBundle == null)
                {
                    string path = PathConfig.AssetRootBundleFilePath;
                    if (File.Exists(path))
                    {
                        MainBundle = AssetBundle.LoadFromFile(path);
                    }
                    else
                    {
                        return null;
                    }
                        
                }
                return MainBundle;
            }
        }
        //AssetBundle主配置文件
        private AssetBundleManifest Manifest;
        public AssetBundleManifest manifest
        {
            get
            {
                if (Manifest == null)
                {
                    if (mainBundle != null)
                    {
                        Manifest = (AssetBundleManifest)mainBundle.LoadAsset(PathConfig.assetBundleManifest_FileName);
                    }
                    else
                    {
                        Manifest = null;
                    }
                    
                }
                return Manifest;
            }
        }

        private IResourcesLoad localLoader;
        private IResourcesLoad bundleLoader;

        public override void Init()
        {
            base.Init();
            localLoader = new LocalResourcesLoader(this);
            bundleLoader = new BundleResourcesLoader(this);
            gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="afterLoadAct"></param>
        /// <returns></returns>
        public Object LoadResource(string objectName, System.Action<Object> afterLoadAct = null)
        {
            Object obj = null;
            if (mainBundle != null)
            {
                obj = bundleLoader.LoadResource(objectName, null);
                if (obj == null)
                {
                    obj = localLoader.LoadResource(objectName, null);
                }
            }
            else
            {
                obj = localLoader.LoadResource(objectName, null);
            }

            if (afterLoadAct != null)
            {
                afterLoadAct(obj);
            }
            
            return obj;
        }
        /// <summary>
        /// 加载多个资源
        /// </summary>
        /// <param name="objectNames"></param>
        /// <param name="afterLoadAct"></param>
        /// <returns></returns>
        public Object[] LoadResources(string[] objectNames, System.Action<Object[]> afterLoadAct = null)
        {
            int length = objectNames.Length;
            Object[] objs = new Object[length];

            for (int i = 0; i < length; i ++)
            {
                objs[i] = ResourcesLoaderHelper.Instance.LoadResource(objectNames[i]);
            }

            if(afterLoadAct != null)
                afterLoadAct(objs);
            return objs;
        }
        /// <summary>
        /// 加载游戏资源
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="afterLoadAct"></param>
        /// <returns></returns>
        public GameObject LoadAndGetInstance(string objectName, System.Action<GameObject> afterLoadAct = null)
        {
            Object obj = ResourcesLoaderHelper.Instance.LoadResource(objectName);
            GameObject go = GameObject.Instantiate(obj) as GameObject;
            go.name = StringEx.GetFileNameWithoutExtention(objectName);
            if (afterLoadAct != null)
                afterLoadAct(go);
            return go;
        }
        /// <summary>
        /// 加载文本资源
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="afterLoadAct"></param>
        /// <returns></returns>
        public TextAsset LoadTextAsset(string objectName, System.Action<TextAsset> afterLoadAct = null)
        {
            TextAsset go = ResourcesLoaderHelper.Instance.LoadResource(objectName, null) as TextAsset;

            if (afterLoadAct != null)
            {
                afterLoadAct(go);
            }

            return go;
        }

        public T LoadResource<T>(string objectName, System.Action<T> afterLoadAct = null) where T : class
        {
            T obj = ResourcesLoaderHelper.Instance.LoadResource(objectName) as T;
            if (afterLoadAct != null)
            {
                afterLoadAct(obj);
            }
            return obj;
        }


        //加载资源列表
        public static Dictionary<string, string> LoadResourcesListFile()
        {
            Dictionary<string, string> resList = new Dictionary<string, string>();
            TextAsset textAsset = Resources.Load(PathConfig.resourceListDocPath) as TextAsset;
            string listData = textAsset.text;
            XDocument resourcesListDoc = XDocument.Parse(listData);
            int i = 1;
            string name = "";
            string path = "";
            foreach (XElement el in resourcesListDoc.Element("Root").Elements())
            {
                if (i % 2 == 1)
                {
                    name = el.Value;
                    //Debug.Log("Name:" + name);
                }
                else
                {
                    path = el.Value;
                    resList.Add(name, path);
                    //Debug.Log("Path:" + path);
                }
                i++;
            }
            return resList;
        }

        public void LoadTextResourcesByPath(string path, System.Action<string, bool> afterAct)
        {
            StartCoroutine(DoDownload(path, afterAct));
        }

        private IEnumerator DoDownload(string path, System.Action<string, bool> afterAct)
        {
            WWW www = new WWW(path);

            yield return www;

            if (www.isDone)
            {
                afterAct(www.text, true);
            }
            else
            {
                afterAct(null, false);
            }
        }

        /// <summary>
        /// 获取Resources文件夹下资源所生成的AssetBundle的路径（即可以直接加载的物体）
        /// </summary>
        /// <param name="objName">物体名称：如“Cube.prefab”</param>
        /// <returns></returns>
        public static string GetResourcesBundlePathByObjectName(string objName)
        {
            return PathConfig.bundleRootPath + "/" + ResourcesLoaderHelper.resourcesList[objName] + Path.GetExtension(objName) + ExName;
        }
        /// <summary>
        /// 获取Resources文件夹下资源所生成的AssetBundle配置文件的路径（即可以直接加载的物体）
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static string GetResourcesBundleManifestPathByObjectName(string objName)
        {
            return GetResourcesBundlePathByObjectName(objName) + PathConfig.assetBundleManifest_ExName;
        }

        /// <summary>
        /// 获取Resources文件夹下资源所生成的AssetBundle的名称
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static string GetResourcesBundleNameByObjectName(string objName)
        {
            if (ResourcesLoaderHelper.resourcesList.ContainsKey(objName))
            {
                return (ResourcesLoaderHelper.resourcesList[objName]).ToLower() + Path.GetExtension(objName) + ExName;
            }
            else
            {
                Debug.logger.Log("找不到资源", objName);
                return null;
            }
        }

        /// <summary>
        /// 通过bundle名来获取相应的bundle路径
        /// </summary>
        /// <param name="bundleName">bundle名称 如“assets/resources/prefabs/cube”</param>
        /// <returns></returns>
        public static string GetBundlePathByBundleName(string bundleName)
        {
            return PathConfig.bundleRootPath + "/" + bundleName;
        }

        /// <summary>
        /// 相对于Assets文件夹的路径转换为根目录于Ressources文件夹的路径
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public static string GetResourcesPath(string dataPath)
        {
            return dataPath.Replace(PathConfig.assetsToResources, "");
        }

    }

}
