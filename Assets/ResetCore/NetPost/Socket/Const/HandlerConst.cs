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
            //通常消息Id为 1~100
            TestHandler = 1,
            RegistChannelHandler = 2,//将用户连接到频道中
            NetObjectJoinUpHandler = 3,//场景物体注册到场景中
            RequsetSceneHandler = 4,//请求场景
            NetObjectRemoveHandler = 5,//从场景中移除物体

            //NetBehavior消息Id为1XX
            NetTransform = 101,//Transform
        }

        public static Dictionary<RequestId, NetPackageHandler> handlerDict = new Dictionary<RequestId, NetPackageHandler>()
        {
            {RequestId.TestHandler, new TestHandler()},
            {RequestId.RegistChannelHandler, new ResistChannelHandler()},

            {RequestId.NetTransform, new BaseNetObjectHandler()},
        };

        
    }

}
