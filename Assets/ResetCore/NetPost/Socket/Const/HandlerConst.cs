using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    }

}
