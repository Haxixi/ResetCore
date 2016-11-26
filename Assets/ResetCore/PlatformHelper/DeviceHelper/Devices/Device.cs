using UnityEngine;
using System.Collections;

namespace ResetCore.PlatformHelper
{
    public abstract class Device
    {

        protected Device() { }

        /// <summary>
        /// 工厂模式创建Device
        /// </summary>
        /// <returns></returns>
        public static Device GetDevice()
        {
            if (Application.platform == RuntimePlatform.Android)
                return new AndroidDevice();
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return new IOSDevice();
            else if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
                return new PCDevice();
            return null;
        }

        /// <summary>
        /// 发送消息，以Json形式发送
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="json"></param>
        public abstract void SendMessage(string eventName, string json = "");

        /// <summary>
        /// 重启应用
        /// </summary>
        public void RestartApp()
        {
            SendMessage(DeviceCommand.RESTART);
        }

        /// <summary>
        /// 安装应用
        /// </summary>
        public void InstallApp(string path)
        {
            SendMessage(DeviceCommand.RESTART, path);
        }

    }

}
