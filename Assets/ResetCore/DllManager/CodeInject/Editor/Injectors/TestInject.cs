using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using System.Linq;
using Mono.Cecil.Cil;

namespace ResetCore.ReAssembly
{
    public class TestInject : BaseInjector
    {
        public override void DoInjectMethod(AssemblyDefinition assembly, MethodDefinition method, TypeDefinition type)
        {
            if (!InjectEmitHelper.HasBodyAndIsNotContructor(method)) return;

            var firstIns = method.Body.Instructions.First();
            var worker = method.Body.GetILProcessor();

            var hasPatchRef = assembly.MainModule.Import(typeof(Debug).GetMethod("Log", new Type[] { typeof(string) }));
            var current = InjectEmitHelper.InsertBefore(worker, firstIns, worker.Create(OpCodes.Ldstr, "Inject"));
            current = InjectEmitHelper.InsertBefore(worker, firstIns, worker.Create(OpCodes.Call, hasPatchRef));
            InjectEmitHelper.ComputeOffsets(method.Body);
        }
    }
}
