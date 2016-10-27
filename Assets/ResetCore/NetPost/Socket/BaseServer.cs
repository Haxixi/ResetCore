using UnityEngine;
using System.Collections;
using System;
using ResetCore.Event;
using ResetCore.Util;

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
        private PackageReciver tcpReciver = new PackageReciver();
        //Udp包接收器
        private PackageReciver udpReciver = new PackageReciver();

        //tcp同步锁
        private object tcpLockObject = new object();
        //udp同步锁
        private object udpLockObject = new object();

        //行为队列
        private ActionQueue handleQueue = new ActionQueue();

        public BaseServer()
        {
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

            CoroutineTaskManager.Instance.LoopTodoByWhile(tcpReciver.HandlePackageInQueue, 0.1f, () => !tcpReciver.Equals(null));
            CoroutineTaskManager.Instance.LoopTodoByWhile(udpReciver.HandlePackageInQueue, 0.1f, () => !udpReciver.Equals(null));
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
        public void Send<T>(int eventId, T value, SendType sendType)
        {
            Package pkg = Package.MakePakage<T>(eventId, value);
            if (sendType == SendType.TCP)
            {
                //TODO
                tcpSocket.Send(pkg.totalData);
            }
            else
            {
                //TODO
                udpSocket.Send(pkg.totalData, pkg.totalLength);
            }
        }

        /// <summary>
        /// 服务器断开连接
        /// </summary>
        public void Disconnect()
        {
            tcpSocket.Disconnect();
            udpSocket.Stop();

            tcpReciver.Reset();
            udpReciver.Reset();
        }

        private void Update()
        {

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
                udpReciver.ReceivePackage(len, data);
            }
        }

        #endregion Udp回调行为

    }
}
