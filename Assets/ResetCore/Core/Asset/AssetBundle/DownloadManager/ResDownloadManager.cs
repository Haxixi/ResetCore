using UnityEngine;
using System.Collections;
using ResetCore.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using ResetCore.Util;

namespace ResetCore.Asset
{
    public class ResDownloadManager
    {
        public void CheckVersion()
        {
            Version resVersion = VersionManager.instance.versionData.resVersion;
            List<Version> verionToDownloadList = new List<Version>();

            DownloadManager.instance.AsynDownLoadText(PathConfig.versionInfoUrl,
                (info) =>
                {
                    Debug.Log(info);
                    XDocument xDoc = XDocument.Parse(info);
                    List<string> versionStrList = xDoc.ReadListByStrFromXML<string>(new string[0]);
                    
                    foreach(XElement el in xDoc.Root.Elements())
                    {
                        Version newVersion = Version.GetValue(el.Value);
                        if (resVersion.Compare(resVersion, newVersion) > 0)
                        {
                            verionToDownloadList.Add(newVersion);
                        }
                    }

                }, () =>
                {
                    Debug.logger.Log("下载出错");
                });

        }

    }
}
