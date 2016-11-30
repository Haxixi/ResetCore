using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ResetCore.NetPost
{
    public static class HandlerConst
    {

        public enum RequestId
        {
            TestHandler = 1,
            RegistChannelHandler = 2,
            NetObjectJoinUpHandler = 3,
        }

        public static Dictionary<RequestId, Handler> handlerDict = new Dictionary<RequestId, Handler>()
        {
            {RequestId.TestHandler, new TestHandler()},
            {RequestId.RegistChannelHandler, new ResistChannelHandler()},
        };
    }

}
