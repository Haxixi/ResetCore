using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResetCore.ReAssembly
{
    //需要注入的函数或者类
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute
    {
        public List<string> injectNameList { get; private set; }
        public InjectAttribute(params string[] injectNames)
        {
            injectNameList = new List<string>(injectNames);
        }
    }

    //需要忽略注入的类或者函数
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IgnoreInjectAttribute : Attribute
    {
        public List<string> ignoreinjectNameList { get; private set; }
        public IgnoreInjectAttribute(params string[] ignoreNames)
        {
            ignoreinjectNameList = new List<string>(ignoreNames);
        }
    }
}
