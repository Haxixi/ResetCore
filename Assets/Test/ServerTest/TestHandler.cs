using UnityEngine;
using System.Collections;
using System;
using ResetCore.Util;
using ResetCore.Event;

namespace ResetCore.NetPost
{
    public class TestHandler : Handler
    {
        protected override void Handle(Package package, Action act = null)
        {
            Vector3D.Vector3DData vec = package.GetValue<Vector3D.Vector3DData>();
            Debug.LogError(vec.X + " " + vec.Y + " " + vec.Z);
            EventDispatcher.TriggerEvent<Vector3D.Vector3DData>("TestHandler", vec);
            if (act != null)
            {
                act();
            }
        }
    }
}
