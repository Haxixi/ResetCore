using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using ResetCore.Util;
using UnityEngine.UI;
using System.Text;
using ResetCore.CodeDom;

namespace ResetCore.UGUI
{


    public class UIScriptGener
    {

        
        private GameObject rootGo;
        private Type uiType;
        public void GenScript(BaseUI ui)
        {
            rootGo = ui.gameObject;
            uiType = ui.GetType();
            string viewClassName = uiType.Name + "View";
            CodeGener gener = new CodeGener(UIView.uiViewNameSpace, viewClassName);
            gener.AddBaseType("UIView")
                .AddImport("ResetCore.UGUI");

            rootGo.transform.DoToSelfAndAllChildren((tran) =>
            {
                GameObject go = tran.gameObject;
                if (!go.name.StartsWith(UIView.genableSign)) return;

                string goName = go.name.Replace(UIView.genableSign, string.Empty);

                gener.AddMemberProperty(typeof(GameObject), UIView.goName + goName);
                var coms = go.GetComponents<Component>();
                foreach(var com in coms)
                {
                    Type comType = com.GetType();
                    if (!UIView.uiCompTypeList.Contains(comType)) continue;

                    gener.AddMemberProperty(comType, UIView.comNameDict[comType] + goName);
                }

            });
            gener.GenCSharp(PathEx.Combine(Application.dataPath, UIView.uiViewScriptPath));
        }

      
    }

}
