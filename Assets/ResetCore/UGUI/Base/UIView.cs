using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine.UI;
using ResetCore.Util;
using System.Reflection;

namespace ResetCore.UGUI
{
    public class UIView
    {
        //有效的组件
        public List<Type> uiCompTypeList = new List<Type>()
        {
            typeof(Image),
            typeof(Button),
            typeof(Text),
        };
        //前缀
        private static readonly string genableSign = "g-";

        private Dictionary<string, Component> comDict;

        private MonoBehaviour rootComponent;

        private Type rootType;

        //初始化
        public UIView(MonoBehaviour root)
        {
            rootComponent = root;
            rootType = root.GetType();
            comDict = new Dictionary<string, Component>();
            GetUIComponents(root.gameObject);
        }


        /// <summary>
        /// 获取UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetUI<T>(string name) where T : Component
        {
            if (comDict.ContainsKey(name))
            {
                return (comDict[name]) as T;
            }
            else
            {
                Debug.logger.LogError("组件获取错误" , name + "不存在");
                return null;
            }
        }

        /// <summary>
        /// 直接通过名字获取（用到反射，不要再Update中进行调用）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetUIByName<T>(string name) where T : Component
        {
            string finalName = name + "_" + typeof(T).Name;
            if (comDict.ContainsKey(finalName))
            {
                return (comDict[finalName]) as T;
            }
            else
            {
                Debug.logger.LogError("组件获取错误", finalName + "不存在");
                return null;
            }
        }

        /// <summary>
        /// 将必要组件加入
        /// </summary>
        /// <param name="go"></param>
        private void GetUIComponents(GameObject go)
        {
            var coms = go.GetComponents<Component>();
            foreach (var com in coms)
            {
                Type comType = com.GetType();
                if (uiCompTypeList.Contains(comType) && com.gameObject.name.StartsWith(genableSign))
                {
                    string comGoName = com.gameObject.name.Replace("g-", "");

                    StringBuilder builder = new StringBuilder();
                    string name = builder.Append(comGoName).Append("_").Append(comType.Name).ToString();
                    if (!comDict.ContainsKey(name))
                    {
                        comDict.Add(name, com);
                    }
                    else
                    {
                        Debug.LogError("重名！" + name);
                    }

                    if(com is Button)
                    {
                        Button btn = com as Button;
                        MethodInfo method = rootType.GetMethod("On" + comGoName);
                        if(method != null)
                        {
                            UIEventListener.Get(btn.gameObject).onClick = (btnGo) =>
                            {
                                method.Invoke(rootComponent, new object[0]);
                            };
                        }
                    }
                }
            }
            if (go.transform.childCount > 0)
            {
                go.transform.DoToAllChildren((tran) =>
                {
                    GetUIComponents(tran.gameObject);
                });
            }
        }
    }

}

