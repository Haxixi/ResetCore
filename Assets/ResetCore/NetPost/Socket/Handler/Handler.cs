using UnityEngine;
using System.Collections;
using System;
using ResetCore.Event;

namespace ResetCore.NetPost
{
    public abstract class Handler
    {
        /// <summary>
        /// 处理服务器
        /// </summary>
        protected BaseServer ownerServer;
        /// <summary>
        /// 处理函数
        /// </summary>
        /// <param name="package"></param>
        /// <param name="act"></param>
        protected abstract void Handle(Package package, Action act = null);

        /// <summary>
        /// 处理包行为，分配给相应的Handler
        /// </summary>
        /// <param name="package"></param>
        /// <param name="act"></param>
        public static void HandlePackage(BaseServer server, Package package, Action act = null)
        {
            HandlerConst.RequestId id = EnumEx.GetValue<HandlerConst.RequestId>(package.eventId);
            if (HandlerConst.handlerDict.ContainsKey(id))
            {
                HandlerConst.handlerDict[id].ownerServer = server;
                HandlerConst.handlerDict[id].Handle(package, act);
            }
            else
            {
                Debug.logger.LogError("NetPost", "不存在id：" + id.ToString());
            }
			if (act != null) {
				act ();
			}
        }

    }

   
}
