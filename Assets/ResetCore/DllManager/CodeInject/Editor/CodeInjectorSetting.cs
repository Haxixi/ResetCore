using Mono.Cecil;
using ResetCore.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ResetCore.ReAssembly
{
    public class CodeInjectorSetting
    {
        private readonly List<string> assemblys = new List<string>();
        public string buildTarget { get; set; }
        public string outputDirectory { get; set; }

        /// <summary>
        /// 用于复用注入器
        /// </summary>
        private static ObjectPool<string, BaseInjector> injectorPool = ObjectPool.CreatePool<BaseInjector>("InjectorPool");


        public CodeInjectorSetting(string target, string outputPath)
        {
            this.buildTarget = target;
            this.outputDirectory = outputPath;
        }

        /// <summary>
        /// 进行注入
        /// </summary>
        public void RunInject()
        {
            foreach(var dllPath in assemblys)
            {

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
            var outPath = Path.Combine(outputDirectory, Path.GetFileName(path));
            Debug.Log(string.Format("WriteAssembly: {0}", outPath));

            var writerParameters = new WriterParameters { WriteSymbols = true };
            assembly.Write(outPath, writerParameters);
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
                            if (!injectorPool.ContainsKey(injectKey))
                            {
                                injectorPool.Put(injectKey, 
                                    Activator.CreateInstance(AssemblyManager.GetDefaultAssemblyType("ResetCore.ReAssembly." + injectKey)) as BaseInjector);
                            }
                            injectorPool.Get(injectKey).DoInjectMethod(assembly, method, type);
                        }

                        //DoInjectMethod(assembly, method, type);
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
                            if (!injectorPool.ContainsKey(injectKey))
                            {
                                injectorPool.Put(injectKey,
                                    Activator.CreateInstance(AssemblyManager.GetDefaultAssemblyType("ResetCore.ReAssembly." + injectKey)) as BaseInjector);
                            }
                            injectorPool.Get(injectKey).DoInjectMethod(assembly, method, type);
                        }

                        modified = true;
                    }
                }
            }
            return modified;
        }
    }

}
