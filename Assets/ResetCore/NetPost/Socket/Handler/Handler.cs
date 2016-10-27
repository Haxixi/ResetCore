using UnityEngine;
using System.Collections;
using System;

namespace ResetCore.NetPost
{
    public abstract class Handler
    {
        protected abstract void Handle(Package package, Action act = null);

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
