using UnityEngine;
using System.Collections;
using ResetCore.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using ResetCore.Util;
using System;
using System.IO;

namespace ResetCore.Asset
{
    public class ResDownloadManager
    {
        //信息行为
        private Action<string> infoAct;
        //进度
        private Action<float> progressAct;
        //完成后行为
        private Action<bool> finishAct;
        //错误处理行为
        private Action<Exception> errorAct;
        //应用需要升级行为
        private Action<Version> appUpdateAct;


        public void CheckVersion(Action<string> infoAct, Action<float> progressAct
            , Action<bool> finishAct, Action<Version> appUpdateAct, Action<Exception> errorAct)
        {
            this.infoAct = infoAct;
            this.progressAct = progressAct;
            this.finishAct = finishAct;
            this.errorAct = errorAct;
            this.appUpdateAct = appUpdateAct;

            if (!CheckNet())
            {
                
                finishAct(false);
                return;
            }
            //下载汇总信息
            string info = DownloadVersionInfo();
            if (info == null)
            {
                finishAct(false);
                return;
            }
            //获取更新版本列表
            List<Version> verionToDownloadList = GetNewVersionList(info);
            verionToDownloadList.Sort(Version.Compare);
            //下载新版本资源
            DownloadVersionResources(verionToDownloadList);
        }
        //检查网络
        private bool CheckNet()
        {
            bool hasNet = !(Application.internetReachability == NetworkReachability.NotReachable);
            if (!hasNet)
            {
                infoAct("Please check your network");
                Debug.logger.LogError("net error", "Please check your network");
            }
            return hasNet;
        }
        //下载汇总更新信息
        private string DownloadVersionInfo()
        {
            infoAct("Downloading VersionInfo");
            string info = DownloadManager.Instance.DownLoadText(PathConfig.versionInfoUrl, errorAct);
            if (string.IsNullOrEmpty(info))
            {
                infoAct("Download VersionInfo Fail");
                Debug.logger.LogError("Download Fail", "Download VersionInfo Fail");
                return null;
            }
            infoAct("Download VersionInfo Successed");
            return info;
        }
        //获取更新列表
        private List<Version> GetNewVersionList(string info)
        {
            Version resVersion = VersionManager.Instance.versionData.resVersion;
            List<Version> verionToDownloadList = new List<Version>();

            XDocument xDoc = XDocument.Parse(info);

            foreach (XElement el in xDoc.Root.Elements())
            {
                Version newVersion = Version.GetValue(el.Value);
                if (Version.Compare(resVersion, newVersion) < 0)
                {
                    verionToDownloadList.Add(newVersion);
                }
            }

            return verionToDownloadList;
        }
        //依次下载版本
        private void DownloadVersionResources(List<Version> verionToDownloadList)
        {
            //下载列表
            List<VersionData> resToDownload = new List<VersionData>();
            foreach (Version ver in verionToDownloadList)
            {
                Version version = ver;
                VersionData data = DownloadVersionData(version);
                if (data == null)
                {
                    finishAct(false);
                }
                else
                {
                    resToDownload.Add(data);
                }
            }
            AddDownloadTask(resToDownload);
        }

        //获取每个版本包的信息
        private VersionData DownloadVersionData(Version version)
        {
            //下载版本信息
            infoAct("Download VersionInfo " + version.ToString());
            string versionDataPath = 
                Path.Combine(PathConfig.GetBundleResourcesFolderByVersion(version), PathConfig.VersionDataName + ".xml");
            string versionData = DownloadManager.Instance.DownLoadText(versionDataPath, errorAct);

            if (versionData == null)
            {
                infoAct("Download VersionInfo Fail");
                Debug.logger.LogError("Download Fail", "Download VersionInfo Fail");
                return null;
            }

            VersionData data = VersionData.ParseXml(XDocument.Parse(versionData));
            //检查应用版本信息
            if (!CheckAppVersion(data))
            {
                infoAct("You need update your app");
                appUpdateAct(data.appVersion);
                return null;
            }
            return data;
        }

        //检查应用版本
        private bool CheckAppVersion(VersionData data)
        {
            return Version.Compare(VersionManager.Instance.versionData.appVersion, data.appVersion) >= 0;
        }

        private void AddDownloadTask(List<VersionData> resToDownload)
        {
            try
            {
                
                foreach (VersionData data in resToDownload)
                {
                    string url = Path.Combine(PathConfig.GetBundleResourcesFolderByVersion(data.resVersion)
                        ,data.resVersion.ToString() + AssetBundleConst.packageEx);
                    Debug.logger.Log("url：" + url);
                    string filePath = 
                        Path.Combine(PathConfig.bundleRootPath, data.resVersion.ToString() + AssetBundleConst.packageEx);
                    Debug.logger.Log("filePath：" + filePath);
                    PathEx.MakeDirectoryExist(filePath);

                    DownloadManager.Instance.AddNewDownloadTask(url, filePath, 
                        data.MD5, progressAct, 
                        (comp)=> 
                        {
                            Decompress(filePath);
                            Debug.Log(PathConfig.LocalVersionDataInPersistentDataPath);
                            data.GenXml(PathConfig.LocalVersionDataInPersistentDataPath);
                        });
                }
                infoAct("Begin Download");
                DownloadManager.Instance.CheckDownLoadList(
                    () =>
                    {
                        infoAct("Download Finish");
                        finishAct(true);
                    },
                     (now, sum, info) =>
                     {
                         infoAct("downloading");
                     });
            }
            catch (Exception ex)
            {
                finishAct(false);
                errorAct(ex);
                return;
            }
        }

        //解压
        private void Decompress(string filePath)
        {
            infoAct("DeCompress Download");
            CompressHelper.DecompressToDirectory(Path.GetDirectoryName(filePath), filePath);
            File.Delete(filePath);
        }
    }
}
