using Mono.Cecil;
using ResetCore.Asset;
using ResetCore.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;

namespace ResetCore.ReAssembly
{
    public class CodeInjectorSetting
    {
        private readonly List<string> assemblys = new List<string>();
        private readonly List<string> injectTags = new List<string>();
        private readonly XDocument injectDllConfigXml;


        /// <summary>
        /// 用于复用注入器
        /// </summary>
        private static ObjectPool<string, BaseInjector> injectorPool = ObjectPool.CreatePool<BaseInjector>("InjectorPool");

        public CodeInjectorSetting()
        {
            var injectDllConfig = EditorResources.GetAsset<TextAsset>("InjectDll", "CodeInject");
            injectDllConfigXml = XDocument.Parse(injectDllConfig.text);
            //添加dll
            var dllEles = injectDllConfigXml.Root.XPathSelectElements("Common/item/dll");
            foreach(var dllEle in dllEles)
            {
                assemblys.Add(dllEle.Value);
            }
            var injectTagEles = injectDllConfigXml.Root.XPathSelectElements("Common/item/injectAttr");
            foreach (var injectTagEle in injectTagEles)
            {
                injectTags.Add(injectTagEle.Value);
            }
        }


        /// <summary>
        /// 进行注入
        /// </summary>
        public void RunInject()
        {
            foreach(var dllPath in assemblys)
            {
                string path = Path.Combine(PathConfig.projectPath, dllPath);
                var assembly = AssemblyDefinition.ReadAssembly(path);
                DoInjector(assembly, injectTags);
                SaveAssembly(path, assembly);
            }
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="scriptPath"></param>
        public void AddAssembly(string scriptPath)
        {
            assemblys.Add(scriptPath);
        }

        /// <summary>
        /// 读取Assembly
        /// </summary>
        /// <param name="path"></param>
        /// <param name="engineList"></param>
        /// <returns></returns>
        private AssemblyDefinition ReadAssembly(string path, List<string> engineList)
        {
            var assemblyResolver = new DefaultAssemblyResolver();
            foreach(string enginePath in engineList)
            {
                assemblyResolver.AddSearchDirectory(enginePath);
            }
            var readerParameters = new ReaderParameters
            {
                AssemblyResolver = assemblyResolver,
                ReadingMode = ReadingMode.Immediate,
                ReadSymbols = true
            };
            var assembly = AssemblyDefinition.ReadAssembly(path, readerParameters);
            return assembly;
        }

        /// <summary>
        /// 保存Assembly
        /// </summary>
        /// <param name="path"></param>
        /// <param name="assembly"></param>
        private void SaveAssembly(string path, AssemblyDefinition assembly)
        {
            Debug.Log(string.Format("WriteAssembly: {0}", path));
            assembly.Write(path, new WriterParameters());
        }

        /// <summary>
        /// 进行代码注入
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="injectList"></param>
        /// <returns></returns>
        private bool DoInjector(AssemblyDefinition assembly, List<string> injectList)
        {
            var modified = false;
            foreach (var type in assembly.MainModule.Types)
            {
                if (type.HasCustomAttribute<InjectAttribute>())
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.HasCustomAttribute<IgnoreInjectAttribute>()) continue;

                        foreach (string injectKey in injectList)
                        {
                            GetInject(injectKey).DoInjectMethod(assembly, method, type);
                        }

                        modified = true;
                    }
                }
                else
                {
                    foreach (var method in type.Methods)
                    {
                        if (!method.HasCustomAttribute<InjectAttribute>()) continue;

                        foreach (string injectKey in injectList)
                        {
                            GetInject(injectKey).DoInjectMethod(assembly, method, type);
                        }

                        modified = true;
                    }
                }
            }
            return modified;
        }

        /// <summary>
        /// 获取注入器
        /// </summary>
        /// <param name="injectKey"></param>
        /// <returns></returns>
        private BaseInjector GetInject(string injectKey)
        {
            if (!injectorPool.ContainsKey(injectKey))
            {
                //foreach(var assmbly in AppDomain.CurrentDomain.GetAssemblies())
                //{
                //    string name = "ResetCore.ReAssembly." + injectKey;
                //    Debug.Log(AssemblyManager.GetAssembly("Assembly-CSharp-Editor").GetType(name));
                //}
                Type injectorType = AssemblyManager.GetAssemblyType("Assembly-CSharp-Editor", "ResetCore.ReAssembly." + injectKey);
                injectorPool.Put(injectKey,
                    injectorType.GetConstructor(new Type[0]).Invoke(new object[0]) as BaseInjector);
            }
            return injectorPool.Get(injectKey);
        }
    }

}
