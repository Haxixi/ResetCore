//using System;
//using System.IO;
//using System.Xml;
//using System.Collections.Generic;
//using System.Text;

///// <summary>
///// 安卓Menifest文件处理器
///// </summary>
//public class AndroidManifestBuilder
//{
//    [UnityEditor.MenuItem("Project/MergeMenifest")]
//    public static void MergeMenifest()
//    {
//        //Merge("common.xml", "uc.xml");
//        MergeDir("doc");

//        //DirectoryUtility.DeepCopyDir("src", "dst", true, null, "baidu.xml|uc.xml");
//    }

//    public static void MergePermission(AndroidManifest bas, AndroidManifest add)
//    {
//        if (add.PermissionNodes.Count == 0)
//            return;

//        //XmlNode position = bas.LastPermissionNode != null ? bas.LastPermissionNode : null;
//        XmlNode pre = AddComment(bas.ManifestNode, "***********************************来自" + add.Name + "的权限开始***********************************", bas.PermissionEndNode, false);

//        XmlNode last = pre;

//        foreach(XmlNode n in add.PermissionNodes)
//        {
//            XmlNode nd = bas.XML.ImportNode(n, true);

//            if (bas.ContainsPermission(nd.OuterXml))
//            {
//                string s = "【重复】" + nd.OuterXml;
//                s = s.Replace(" xmlns:android=\"http://schemas.android.com/apk/res/android\"", "");
//                nd = AddComment(bas.ManifestNode, s, last);
//            }
                
//            else
//                bas.ManifestNode.InsertAfter(nd, last);

//            last = nd;
//        }

//        AddComment(bas.ManifestNode, "***********************************来自" + add.Name + "的权限结束***********************************", last);
//    }

//    public static void MergeSDK(AndroidManifest bas, AndroidManifest add)
//    {
//        if (add.SDKNode == null)
//            return;

//        if(bas.SDKNode == null)
//        {
//            XmlNode node = bas.XML.ImportNode(add.SDKNode, true);
//            bas.ManifestNode.InsertAfter(node, null);
//        }
//        else
//        {
//            XmlNode node = bas.XML.ImportNode(add.SDKNode, true);
//            bas.ManifestNode.InsertAfter(node, bas.SDKNode.PreviousSibling);
//            bas.ManifestNode.RemoveChild(bas.SDKNode);
//        }
//    }

//    public static void MergeFeature(AndroidManifest bas, AndroidManifest add)
//    {
//        if (add.FeatureNodes.Count == 0)
//            return;

//        XmlNode pre = AddComment(bas.ManifestNode, "***********************************来自" + add.Name + "的特性开始***********************************", bas.FeatureEndNode, false);

//        XmlNode last = pre;

//        foreach (XmlNode n in add.FeatureNodes)
//        {
//            XmlNode nd = bas.XML.ImportNode(n, true);

//            if (bas.ContainsFeature(nd.OuterXml))
//            {
//                string s = "【重复】" + nd.OuterXml;
//                s = s.Replace(" xmlns:android=\"http://schemas.android.com/apk/res/android\"", "");
//                nd = AddComment(bas.ManifestNode, s, last);
//            }

//            else
//                bas.ManifestNode.InsertAfter(nd, last);

//            last = nd;
//        }

//        AddComment(bas.ManifestNode, "***********************************来自" + add.Name + "的特性结束***********************************", last);
//    }

//    public static void MergeScreen(AndroidManifest bas, AndroidManifest add)
//    {
//        if (add.ScreenNode == null)
//            return;

//        if (bas.ScreenNode == null)
//        {
//            XmlNode node = bas.XML.ImportNode(add.ScreenNode, true);
//            bas.ManifestNode.InsertAfter(node, null);
//        }
//        else
//        {
//            XmlNode node = bas.XML.ImportNode(add.ScreenNode, true);
//            bas.ManifestNode.InsertAfter(node, bas.ScreenNode.PreviousSibling);
//            bas.ManifestNode.RemoveChild(bas.ScreenNode);
//        }
//    }

//    public static void MergeApplication(AndroidManifest bas, AndroidManifest add)
//    {
//        if (add.ApplicationNode == null)
//            return;

//        //合并Application的属性
//        foreach (XmlAttribute attr in add.ApplicationNode.Attributes)
//        {
//            bool exist = false;
//            foreach (XmlAttribute bttr in bas.ApplicationNode.Attributes)
//            {
//                if (bttr.Name.ToLower() == attr.Name.ToLower() && bttr.NamespaceURI == attr.NamespaceURI)
//                {
//                    exist = true;
//                    bttr.Value = attr.Value;
//                    break;
//                }
//            }

//            if (!exist)
//            {
//                XmlAttribute at = bas.XML.CreateAttribute(attr.Name, attr.NamespaceURI);
//                at.Value = attr.Value;
//                bas.ApplicationNode.Attributes.Append(at);
//            }

//        }

//        if (bas.ApplicationNode == null)
//        {
//            XmlNode nd = bas.XML.ImportNode(add.ApplicationNode, true);
//            bas.ManifestNode.InsertAfter(nd, null);
//        }
//        else if(add.ApplicationChildren != null && add.ApplicationChildren.Count > 0)
//        {
//            AddComment(bas.ApplicationNode, "***********************************来自" + add.Name + "的界面开始***********************************");
//            foreach(XmlNode nn in add.ApplicationChildren)
//            {
//                XmlNode nd = bas.XML.ImportNode(nn, true);
//                bas.ApplicationNode.AppendChild(nd);
//            }
//            AddComment(bas.ApplicationNode, "***********************************来自" + add.Name + "的界面结束***********************************");
//        }
//    }

//    public static bool MergeDir(string dir, BuildCommand cmd = null, BuildConfig config = null, string common = "common.xml",bool mergetPlatXml=true)
//    {
//        //读取目录信息
//        DirectoryInfo dinfo = new DirectoryInfo(dir);

//        string commonXml = common != null ? common : "common.xml";
//        string platformXml = cmd != null ? cmd.Platform + ".xml" : null;
//        string finalXml = dinfo.FullName + "\\AndroidManifest.xml";

//        //检测基础文件是否存在
//        if (!File.Exists(dinfo.FullName + "\\" + commonXml))
//        {
//            UnityEngine.Debug.LogError("common xml : " + commonXml + " not found");
//            return false;
//        }

//        //解析基础文件
//        AndroidManifest com = new AndroidManifest(dinfo.FullName + "\\" + commonXml);

//        if(!com.IsValid)
//        {
//            UnityEngine.Debug.LogError("common xml : " + commonXml + " format error");
//            return false;
//        }

//        //寻找全部xml文件
//        FileInfo[] files = dinfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly);

//        //合并XML
//        foreach(FileInfo f in files)
//        {
//            if (f.Name != "common.xml" &&
//                f.Name != "AndroidManifest.xml" &&
//                f.Name != commonXml &&
//                f.Name != platformXml)
//                Merge(com, f.FullName);
//        }

//        if(mergetPlatXml)
//        {
//            UnityEngine.Debug.LogError("my platform : " + dinfo.FullName + "\\" + platformXml);

//            //合并本渠道XML
//            if (File.Exists(dinfo.FullName + "\\" + platformXml))
//            {
//                UnityEngine.Debug.LogError("in my platform merge");
//                Merge(com, dinfo.FullName + "\\" + platformXml);
//            }
//            else
//                UnityEngine.Debug.LogError("out my platform merge");
//        }


//        //生成最终文件
//        com.XML.Save(finalXml);

//        //宏替换
//        string content = File.ReadAllText(finalXml);

//        //替换Bundle
//        string bundle = config != null ? config.Bundle : null;
//        if (bundle != null)
//            content = content.Replace("{bundle}", bundle);

//        foreach (string param in config.ParamKeys)
//        {
//            string pv = config.GetParam(param);
//            content = content.Replace("{" + param + "}", pv);
//        }
//        File.WriteAllText(finalXml, content);


//        return true;
//    }

//    /// <summary>
//    /// 合并
//    /// </summary>
//    /// <param name="baseFile"></param>
//    /// <param name="addFile"></param>
//    public static void Merge(string baseFile, string addFile)
//    {
//        AndroidManifest b = new AndroidManifest(baseFile);
//        AndroidManifest a = new AndroidManifest(addFile);

//        IList<XmlNode> nodes = b.UnknownNodes;
//        MergePermission(b, a);
//        MergeSDK(b, a);
//        MergeFeature(b, a);
//        MergeScreen(b, a);
//        MergeApplication(b, a);

//        b.XML.Save("merge.xml");
//    }

//    public static void Merge(AndroidManifest b, string addFile)
//    {
//        AndroidManifest a = new AndroidManifest(addFile);

//        if (!a.IsValid)
//        {
//            UnityEngine.Debug.LogError("file " + addFile + " format error ");
//            return;
//        }
            

//        MergePermission(b, a);
//        MergeSDK(b, a);
//        MergeFeature(b, a);
//        MergeScreen(b, a);
//        MergeApplication(b, a);
//    }

//    public static XmlNode AddComment(XmlNode parent, string name, XmlNode position = null, bool after = true)
//    {
//        XmlComment com = parent.OwnerDocument.CreateComment(name);

//        if (position == null)
//            parent.AppendChild(com);
//        else
//        {
//            if (after)
//                parent.InsertAfter(com, position);
//            else
//                parent.InsertBefore(com, position);
//        }
            
//        return com;
//    }

//    /// <summary>
//    /// 参数替换
//    /// </summary>
//    /// <param name="file">文件</param>
//    /// <param name="name">参数名</param>
//    /// <param name="val">参数值</param>
//    public static void ReplaceParam(string file, string name, string val)
//    {
//        if (!File.Exists(file))
//            return;

//        string content = File.ReadAllText(file);
//        content = content.Replace("{" + name + "}", val);

//        File.Delete(file);
//        File.WriteAllText(file, content, Encoding.UTF8);
//    }
//}

