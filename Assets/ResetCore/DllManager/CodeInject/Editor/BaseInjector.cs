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

        /// <summary>
        ///     语句后插入Instruction, 并返回当前语句
        /// </summary>
        public static Instruction InsertAfter(ILProcessor worker, Instruction target, Instruction instruction)
        {
            worker.InsertAfter(target, instruction);
            return instruction;
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
    }
}
