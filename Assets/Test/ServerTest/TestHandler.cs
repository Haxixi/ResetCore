using UnityEngine;
using System.Collections;
using System;
using ResetCore.Util;

namespace ResetCore.NetPost
{
    public class TestHandler : Handler
    {
        protected override void Handle(Package package, Action act = null)
        {
            Vector3D.Vector3DData vec = package.GetValue<Vector3D.Vector3DData>();
            Debug.Log(vec.X + " " + vec.Y + " " + vec.Z);
            if (act != null)
            {
                act();
            }
        }
    }
}
