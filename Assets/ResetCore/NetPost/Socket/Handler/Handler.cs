using UnityEngine;
using System.Collections;
using System;

namespace ResetCore.NetPost
{
    public abstract class Handler
    {
        protected abstract void Handle(Package package, Action act = null);

        /// <summary>
        /// 处理包行为，分配给相应的Handler
        /// </summary>
        /// <param name="package"></param>
        /// <param name="act"></param>
        public static void HandlePackage(Package package, Action act = null)
        {
            HandlerConst.HandlerId id = EnumEx.GetValue<HandlerConst.HandlerId>(package.eventId);
            if (HandlerConst.handlerDict.ContainsKey(id))
            {
                HandlerConst.handlerDict[id]
                .Handle(package, act);
            }
            else
            {
                Debug.LogError("不存在id：" + id.ToString());
            }
            
        }

    }

   
}
