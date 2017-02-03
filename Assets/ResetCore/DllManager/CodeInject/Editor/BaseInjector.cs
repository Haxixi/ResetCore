using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ResetCore.ReAssembly
{
    /// <summary>
    /// 基于属性的注入器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseInjector
    {
        /// <summary>
        /// 执行注入操作
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="method"></param>
        /// <param name="type"></param>
        public abstract void DoInjectMethod(AssemblyDefinition assembly, MethodDefinition method, TypeDefinition type);

    }

    /// <summary>
    /// 用于编写Emit
    /// </summary>
    public static class InjectEmitHelper
    {
        /// <summary>
        ///     语句前插入Instruction, 并返回当前语句
        /// </summary>
        public static Instruction InsertBefore(ILProcessor worker, Instruction target, Instruction instruction)
        {
            worker.InsertBefore(target, instruction);
            return instruction;
        }

        public static Instruction InsertBefore(ILProcessor worker, Instruction target, Dictionary<OpCode, object> injectDictionary)
        {
            Instruction current = null;
            foreach (var injectKvp in injectDictionary)
            {
                if(injectKvp.Value == null){
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key));
                }
                else if(injectKvp.Value is MethodReference)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (MethodReference)injectKvp.Value));
                }
                else if (injectKvp.Value is CallSite)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (CallSite)injectKvp.Value));
                }
                else if (injectKvp.Value is TypeReference)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (TypeReference)injectKvp.Value));
                }
                else if (injectKvp.Value is ParameterDefinition)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (ParameterDefinition)injectKvp.Value));
                }
                else if (injectKvp.Value is VariableDefinition)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (VariableDefinition)injectKvp.Value));
                }
                else if (injectKvp.Value is Instruction)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (Instruction)injectKvp.Value));
                }
                else if (injectKvp.Value is long)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (long)injectKvp.Value));
                }
                else if (injectKvp.Value is int)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (int)injectKvp.Value));
                }
                else if (injectKvp.Value is byte)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (byte)injectKvp.Value));
                }
                else if (injectKvp.Value is sbyte)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (sbyte)injectKvp.Value));
                }
                else if (injectKvp.Value is string)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (string)injectKvp.Value));
                }
                else if (injectKvp.Value is FieldReference)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (FieldReference)injectKvp.Value));
                }
                else if (injectKvp.Value is byte)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (byte)injectKvp.Value));
                }
                else if (injectKvp.Value is float)
                {
                    current = InsertBefore(worker, target, worker.Create(injectKvp.Key, (float)injectKvp.Value));
                }
                else
                {
                    Debug.logger.LogError("OpCode", "参数错误");
                }
            }
            return current;
        }

        /// <summary>
        ///     语句后插入Instruction, 并返回当前语句
        /// </summary>
        public static Instruction InsertAfter(ILProcessor worker, Instruction target, Instruction instruction)
        {
            worker.InsertAfter(target, instruction);
            return instruction;
        }

        /// <summary>
        /// 输出语句方法
        /// </summary>
        private static MethodReference logMethod = null;
        /// <summary>
        /// 插入输出语句
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="worker"></param>
        /// <param name="target"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Instruction InsertLogBefore(AssemblyDefinition assembly, ILProcessor worker, Instruction target, string text)
        {
            if(logMethod == null)
                logMethod = assembly.MainModule.Import(typeof(Debug).GetMethod("Log", new Type[] { typeof(string) }));
            var current = InjectEmitHelper.InsertBefore(worker, target, worker.Create(OpCodes.Ldstr, "Inject"));
            current = InjectEmitHelper.InsertBefore(worker, target, worker.Create(OpCodes.Call, logMethod));
            return current;
        }

        /// <summary>
        /// 计算偏差
        /// </summary>
        /// <param name="body"></param>
        public static void ComputeOffsets(MethodBody body)
        {
            var offset = 0;
            foreach (var instruction in body.Instructions)
            {
                instruction.Offset = offset;
                offset += instruction.GetSize();
            }
        }

        /// <summary>
        /// 拥有函数体并且不为构造函数
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool HasBodyAndIsNotContructor(MethodDefinition method)
        {
            return !method.Name.Equals(".ctor") && method.HasBody;
        }

        /// <summary>
        /// 是否为getter或者setter
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsGetterOrSetter(MethodDefinition method)
        {
            return method.IsGetter || method.IsSetter;
        }
    }
}
