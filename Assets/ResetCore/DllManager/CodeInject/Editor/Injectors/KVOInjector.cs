#if EVENT
using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using System.Linq;
using ResetCore.Event;
using Mono.Cecil.Cil;

namespace ResetCore.ReAssembly
{
    public class KVOInjector : BaseInjector
    {
        public override void DoInjectMethod(AssemblyDefinition assembly, MethodDefinition method, TypeDefinition type)
        {
            if (!method.IsSetter && !method.IsGetter)
                return;

            if (method.IsGetter)
            {
                //var firstIns = method.Body.Instructions.First();
                //var worker = method.Body.GetILProcessor();

                //var importMethod = (from m in typeof(EventDispatcher).GetMethods() 
                //                   where m.GetParameters().Count() == 3 && m.Name == "TriggerEvent" select m).First();
                //var triggerEvent = assembly.MainModule.Import(importMethod);

                //string methodName = method.Name.Replace("get_", "");
                //string eventName = type.Name + "." + methodName + ".Getter";

                //var field = (from f in type.Fields where f.Name == "<" + methodName + ">k__BackingField" select f).First();

                //var current = InjectEmitHelper.InsertBefore(worker, firstIns,
                //    new Dictionary<OpCode, object>() {
                //        { OpCodes.Ldstr, eventName},
                //        { OpCodes.Ldarg_0, null },
                //        { OpCodes.Ldfld, field },
                //        { OpCodes.Call, triggerEvent},

                //    });
                //InjectEmitHelper.ComputeOffsets(method.Body);
            }

            if (method.IsSetter)
            {
                //var triggerEvent = assembly.MainModule.Import(typeof(EventDispatcher).GetMethod("TriggerEvent", new Type[] { typeof(string), typeof(object), typeof(object) }));
                //string eventName = type.Name + "." + method.Name + ".Setter";
                //var current = InjectEmitHelper.InsertBefore(worker, firstIns, worker.Create(OpCodes.Ldstr, eventName));

            }
        }
    }
}
#endif