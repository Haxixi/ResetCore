using UnityEngine;
using System.Collections;
using Protobuf.Data;
using System;

namespace ResetCore.NetPost
{
    public class NetTransform : NetBehavior<Transform3DData>
    {
        public override HandlerConst.RequestId handlerId
        {
            get
            {
                return HandlerConst.RequestId.NetTransform;
            }
        }


        public override void Awake()
        {
            base.Awake();
            behaviorData = new Transform3DData();
            behaviorData.InstanceId = gameObject.GetInstanceID();
            behaviorData.LocalPosition = new Vector3DData();
            behaviorData.LocalPosition.X = gameObject.transform.localPosition.x;
            behaviorData.LocalPosition.Y = gameObject.transform.localPosition.y;
            behaviorData.LocalPosition.Z = gameObject.transform.localPosition.z;

            behaviorData.LocalEulerAngle = new Vector3DData();
            behaviorData.LocalEulerAngle.X = gameObject.transform.localEulerAngles.x;
            behaviorData.LocalEulerAngle.Y = gameObject.transform.localEulerAngles.y;
            behaviorData.LocalEulerAngle.Z = gameObject.transform.localEulerAngles.z;

            behaviorData.LocalScale = new Vector3DData();
            behaviorData.LocalScale.X = gameObject.transform.localScale.x;
            behaviorData.LocalScale.Y = gameObject.transform.localScale.y;
            behaviorData.LocalScale.Z = gameObject.transform.localScale.z;
        }

        public override void OnNetUpdate(Package serverPkg)
        {
            base.OnNetUpdate(serverPkg);
            Transform3DData serverData = serverPkg.GetValue<Transform3DData>();

            if(serverData.InstanceId != this.instanceId)
                return;

            gameObject.transform.localPosition = new Vector3(serverData.LocalPosition.X, serverData.LocalPosition.Y, serverData.LocalPosition.Z);
            gameObject.transform.localScale = new Vector3(serverData.LocalScale.X, serverData.LocalScale.Y, serverData.LocalScale.Z);
            gameObject.transform.localEulerAngles = new Vector3(serverData.LocalEulerAngle.X, serverData.LocalEulerAngle.Y, serverData.LocalEulerAngle.Z);
        }
    }

}
