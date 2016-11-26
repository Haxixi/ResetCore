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

            loginChannel.ForEach((channelId) => {
                if (!ownerServer.currentChannel.Contains(channelId))
                {
                    ownerServer.currentChannel.Add(channelId);
                }
            });

            logoutChannel.ForEach((channelId) => {
                if (ownerServer.currentChannel.Contains(channelId))
                {
                    ownerServer.currentChannel.Remove(channelId);
                }
            });

        }
    }
}

