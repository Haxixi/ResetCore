using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using ResetCore.Util;
using UnityEngine.UI;
using System.Text;

namespace ResetCore.UGUI
{


    public class UIScriptGener
    {

        public List<Type> uiCompTypeList = new List<Type>()
        {
            typeof(Image),
            typeof(Button),
            typeof(Text),
        };

        private static readonly string genableSign = "g-";

        //根ui名+当前绑定Go名+组件类型
        private Dictionary<string, Component> componentDict;
        private GameObject rootGo;
        public void GenScript(BaseUI ui)
        {
            componentDict = new Dictionary<string, Component>();
            rootGo = ui.gameObject;
            GetUIComponents(ui.gameObject);
            Debug.LogError(componentDict.ConverToString());
        }

        //获取所有的UI组件
        private void GetUIComponents(GameObject go)
        {
            var coms = go.GetComponents<Component>();
            foreach (var com in coms)
            {
                Type comType = com.GetType();
                if (uiCompTypeList.Contains(comType) && com.gameObject.name.StartsWith(genableSign))
                {
                    StringBuilder builder = new StringBuilder();
                    string name = builder.Append(rootGo.name).Append("_")
                        .Append(com.gameObject.name.Replace("g-", "")).Append("_").Append(comType.Name).ToString();
                    if (!componentDict.ContainsKey(name))
                    {
                        componentDict.Add(name, com);
                    }else
                    {
                        Debug.LogError("重名！" + name);
                    }
                }
            }
            if(go.transform.childCount > 0)
            {
                go.transform.DoToAllChildren((tran) =>
                {
                    GetUIComponents(tran.gameObject);
                });
            }
        }
    }

}
