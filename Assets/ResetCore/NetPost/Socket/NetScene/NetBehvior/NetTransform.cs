using UnityEngine;
using System.Collections;
using Protobuf.Data;

namespace ResetCore.NetPost
{
    public class NetTransform : NetBehavior<NetTransformData>
    {
        NetTransformData data = new NetTransformData();

        public override void Awake()
        {
            base.Awake();
            data.LocalPosition.X = gameObject.transform.localPosition.x;
            data.LocalPosition.Y = gameObject.transform.localPosition.y;
            data.LocalPosition.Z = gameObject.transform.localPosition.z;
            data.LocalEulerAngle.X = gameObject.transform.localEulerAngles.x;
            data.LocalEulerAngle.Y = gameObject.transform.localEulerAngles.y;
            data.LocalEulerAngle.Z = gameObject.transform.localEulerAngles.z;
            data.LocalScale.X = gameObject.transform.localScale.x;
            data.LocalScale.Y = gameObject.transform.localScale.y;
            data.LocalScale.Z = gameObject.transform.localScale.z;
        }

        public override void OnNetUpdate(NetTransformData serverData)
        {
            base.OnNetUpdate(serverData);
            gameObject.transform.localPosition = new Vector3(serverData.LocalPosition.X, serverData.LocalPosition.Y, serverData.LocalPosition.Z);
            gameObject.transform.localScale = new Vector3(serverData.LocalScale.X, serverData.LocalScale.Y, serverData.LocalScale.Z);
            gameObject.transform.localEulerAngles = new Vector3(serverData.LocalEulerAngle.X, serverData.LocalEulerAngle.Y, serverData.LocalEulerAngle.Z);
        }
    }

}
