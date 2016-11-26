using UnityEngine;
using System.Collections;
using System;
using ResetCore.Event;
using ResetCore.Util;
using System.Collections.Generic;
using NetPostUtil;

namespace ResetCore.NetPost
{
    public enum SendType
    {
        TCP,
        UDP
    }

    public class ServerEvent
    {
        public static readonly string TcpOnCloseSocket = "ServerEvent.TcpOnCloseSocket";
        public static readonly string TcpOnConnect = "ServerEvent.TcpOnConnect";
        public static readonly string TcpOnError = "ServerEvent.TcpOnError";
        public static readonly string TcpOnListen = "ServerEvent.TcpOnListen";
        public static readonly string TcpOnReceive = "ServerEvent.TcpOnReceive";
        public static readonly string TcpOnSend = "ServerEvent.TcpOnSend";

        public static readonly string UdpOnBind = "ServerEvent.UdpOnBind";
        public static readonly string UdpOnError = "ServerEvent.UdpOnError";
        public static readonly string UdpOnListen = "ServerEvent.UdpOnListen";
        public static readonly string UdpOnReceive = "ServerEvent.UdpOnReceive";
    }

    public class BaseServer
    {
        //Tcp套接字
        private TcpSocket tcpSocket = new TcpSocket();
        //Udp套接字
        private UdpSocket udpSocket = new UdpSocket();

        //Tcp包接收器
        private PackageReciver tcpReciver;
        //Udp包接收器
        private PackageReciver udpReciver;

        //tcp同步锁
        private object tcpLockObject = new object();
        //udp同步锁
        private object udpLockObject = new object();

        //行为队列
        private ActionQueue handleQueue = new ActionQueue();

        //是否已经连接
        public bool isConnect { get; private set; }

        CoroutineTaskManager.CoroutineTask tcpReciverTask;
        CoroutineTaskManager.CoroutineTask udpReciverTask;

        public List<int> currentChannel { get; private set; }

        public BaseServer()
        {
            
            isConnect = false;

            tcpReciver = new PackageReciver(this);
            udpReciver = new PackageReciver(this);

            tcpSocket.onCloseSocket += new TcpSocketCloseSocketDelegate(TcpOnCloseSocket);
            tcpSocket.onConnect += new TcpSocketConnectDelegate(TcpOnConnect);
            tcpSocket.onError += new TcpSocketErrorDelegate(TcpOnError);
            tcpSocket.onListen += new TcpSocketListenDelegate(TcpOnListen);
            tcpSocket.onReceive += new TcpSocketReceiveDelegate(TcpOnReceive);
            tcpSocket.onSend += new TcpSocketSendDelegate(TcpOnSend);

            udpSocket.onBind += new UdpSocketBindDelegate(UdpOnBind);
            udpSocket.onError += new UdpSocketErrorDelegate(UdpOnError);
            udpSocket.onListen += new UdpSocketListenDelegate(UdpOnListen);
            udpSocket.onReceive += new UdpSocketReceiveDelegate(UdpOnReceive);

            currentChannel = new List<int>();

            tcpReciverTask =
                CoroutineTaskManager.Instance.LoopTodoByWhile(tcpReciver.HandlePackageInQueue, Time.deltaTime, () => { return isConnect; });
            udpReciverTask =
                CoroutineTaskManager.Instance.LoopTodoByWhile(udpReciver.HandlePackageInQueue, Time.deltaTime, () => { return isConnect; });


        }

        #region 服务器公开行为
        /// <summary>
        /// 服务器连接
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <param name="remoteTcpPort"></param>
        /// <param name="remoteUdpPort"></param>
        /// <param name="localUdpPort"></param>
        /// <param name="autoRebind"></param>
        public void Connect(string remoteAddress, int remoteTcpPort
            , int remoteUdpPort, int localUdpPort, bool autoRebind = true)
        {
            isConnect = true;
            bool bindSuccess = 
                udpSocket.BindRemoteEndPoint(remoteAddress, remoteUdpPort, localUdpPort, autoRebind);
            bool beginReceive = udpSocket.BeginReceive();

            if(bindSuccess && beginReceive)
            {
                tcpSocket.Connect(remoteAddress, remoteTcpPort);
                
            }
            else
            {
                Debug.LogError("Udp连接失败！");
            }
            
        }

        /// <summary>
        /// 服务器发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventId"></param>
        /// <param name="value"></param>
        /// <param name="sendType"></param>
        public void Send<T>(int eventId, int channelId, T value, SendType sendType)
        {
            Package pkg = Package.MakePakage<T>(eventId, channelId, value, sendType);
            if (sendType == SendType.TCP)
            {
                tcpSocket.Send(pkg.totalData);
            }
            else
            {
                udpSocket.Send(pkg.totalData, pkg.totalLength);
            }
        }
        public void Send<T>(HandlerConst.HandlerId eventId, int channelId, T value, SendType sendType)
        {
            Send<T>((int)eventId, channelId, value, sendType);
        }

        /// <summary>
        /// 服务器断开连接
        /// </summary>
        public void Disconnect()
        {
            tcpReciverTask.Stop();
            udpReciverTask.Stop();

            isConnect = false;
            tcpSocket.Disconnect();
            udpSocket.Stop();

            tcpReciver.Reset();
            udpReciver.Reset();

        }

        /// <summary>
        /// 注册频道
        /// </summary>
        /// <param name="loginChannelList">要登入的频道</param>
        /// <param name="logoutChannelList">要登出的频道</param>
        public void Regist(List<int> loginChannelList, List<int> logoutChannelList)
        {
            string loginListStr = loginChannelList.ConverToString();
            string logoutListStr = loginChannelList.ConverToString();
            RegistData data = new RegistData();
            data.LoginChannel = loginListStr;
            data.LogoutChannel = logoutListStr;

            Send<RegistData>(HandlerConst.HandlerId.RegistChannelHandler, -1, data, SendType.TCP);
        }

        #endregion

        #region Tcp回调行为

        private void TcpOnCloseSocket(CloseType type, SocketState state, Exception e = null)
        {
            EventDispatcher.TriggerEvent<CloseType, SocketState, Exception>
                (ServerEvent.TcpOnCloseSocket, type, state, e);
            //Todo
        }

        private void TcpOnConnect(Exception e = null)
        {
            EventDispatcher.TriggerEvent<Exception>(ServerEvent.TcpOnConnect, e);
            //Todo
            tcpSocket.BeginReceive();
            Debug.logger.Log("Tcp Socket已连接");
        }

        private void TcpOnError(SocketState state, Exception e = null)
        {
            Debug.logger.LogError("ServerError", "在" + state.ToString() + "下报错");
            Debug.LogException(e);
            EventDispatcher.TriggerEvent<SocketState, Exception>(ServerEvent.TcpOnError, state, e);
            //Todo
            Disconnect();
        }

        private void TcpOnListen(Exception e = null)
        {
            EventDispatcher.TriggerEvent<Exception>(ServerEvent.TcpOnListen, e);
            //Todo
        }

        private void TcpOnReceive(int len, byte[] data)
        {
            EventDispatcher.TriggerEvent<int, byte[]>(ServerEvent.TcpOnReceive, len, data);
            //Todo
            lock (tcpLockObject)
            {
                Debug.logger.Log("Tcp Socket接收包，长度为：" + len);
                tcpReciver.ReceivePackage(len, data);
            }
        }

        private void TcpOnSend(int len)
        {
            EventDispatcher.TriggerEvent<int>(ServerEvent.TcpOnSend, len);
            //Todo
        }
        #endregion Tcp回调行为

        #region Udp回调行为
        private void UdpOnBind(Exception e = null)
        {
            EventDispatcher.TriggerEvent<Exception>(ServerEvent.UdpOnBind, e);
            //Todo
            Debug.logger.Log("Udp Socket已经绑定");
        }

        private void UdpOnError(SocketState state, Exception e = null)
        {
            Debug.logger.LogError("ServerError", "在" + state.ToString() + "下报错");
            Debug.LogException(e);
            EventDispatcher.TriggerEvent<SocketState, Exception>(ServerEvent.UdpOnError, state, e);
            //Todo
            Disconnect();
        }

        private void UdpOnListen(Exception e = null)
        {
            EventDispatcher.TriggerEvent<Exception>(ServerEvent.UdpOnListen, e);
            //Todo
        }

        private void UdpOnReceive(int len, byte[] data, string remoteAddress, int remotePort)
        {
            EventDispatcher.TriggerEvent<int, byte[], string, int>
                (ServerEvent.UdpOnReceive, len, data, remoteAddress, remotePort);
            //Todo
            lock (tcpLockObject)
            {
                Debug.logger.Log("Udp Socket接收包，长度为：" + len);
                udpReciver.ReceivePackage(len, data);
            }
        }

        #endregion Udp回调行为

    }
}
