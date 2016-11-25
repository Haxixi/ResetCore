using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ResetCore.NetPost
{
    public static class HandlerConst
    {

        public enum HandlerId
        {
            TestHandler = 1
        }

        public static Dictionary<HandlerId, Handler> handlerDict = new Dictionary<HandlerId, Handler>()
        {
            {HandlerId.TestHandler, new TestHandler()}
        };

        public static Dictionary<HandlerId, Type> sendDataType = new Dictionary<HandlerId, Type>()
        {
            {HandlerId.TestHandler, typeof(Vector3D.Vector3DData)}
        };

    }

}
