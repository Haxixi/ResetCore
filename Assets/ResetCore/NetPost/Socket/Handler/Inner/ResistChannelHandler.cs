using UnityEngine;
using System.Collections;
using System;
using NetPostUtil;
using ResetCore.Util;
using System.Collections.Generic;

namespace ResetCore.NetPost
{
    public class ResistChannelHandler : Handler
    {
        protected override void Handle(Package package, Action act = null)
        {
            RegistData data = package.GetValue<RegistData>();

            var loginChannel = data.LoginChannel.GetValue<List<int>>();
            var logoutChannel = data.LogoutChannel.GetValue<List<int>>();

            if (loginChannel != null)
            {
                loginChannel.ForEach((channelId) =>
                {
                    if (!ownerServer.currentChannel.Contains(channelId))
                    {
                        ownerServer.currentChannel.Add(channelId);
                    }
                });
            }

            if(logoutChannel != null)
            {
                logoutChannel.ForEach((channelId) =>
                {
                    if (ownerServer.currentChannel.Contains(channelId))
                    {
                        ownerServer.currentChannel.Remove(channelId);
                    }
                });
            }
            
            Debug.logger.Log("频道注册成功");
        }
    }
}

