//using UnityEngine;
//using System.Collections;
//using System.Net.Sockets;
//using System;
//using System.Net;
//using ResetCore.Event;

//namespace ResetCore.NetPost
//{
//    public class TcpSocket
//    {

//        public int remotePort { get; private set; }

//        public string remoteAddress { get; private set; }

//        public int localPort { get; private set; }

//        public string localAddress { get; private set; }

//        private Socket socket;

//        /// <summary>
//        /// 是否连接
//        /// </summary>
//        public bool isConneted
//        {
//            get
//            {
//                if (socket == null)
//                {
//                    return false;
//                }
//                else if (!socket.Connected)
//                {
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//        }

//        private AsyncCallback acceptCallback;
//        private AsyncCallback connectCallback;
//        private AsyncCallback disconnectCallback;
//        private AsyncCallback sendCallback;
//        private AsyncCallback receiveCallback;

//        public TcpSocketListenDelegate onListen { get; private set; }
//        public TcpSocketConnectDelegate onConnect { get; private set; }
//        public TcpSocketDisconnectDelegate onDisconnect { get; private set; }
//        public TcpSocketErrorDelegate onError { get; private set; }
//        public TcpSocketReceiveDelegate onReveive { get; private set; }
//        public TcpSocketSendDelegate onSend { get; private set; }


//        public TcpSocket(Socket socket = null)
//        {
//            acceptCallback = new AsyncCallback(OnAccept);
//            connectCallback = new AsyncCallback(OnConnect);
//            disconnectCallback = new AsyncCallback(OnDisconnect);
//            sendCallback = new AsyncCallback(OnSend);
//            receiveCallback = new AsyncCallback(OnReceive);

//            if(socket != null)
//            {
//                this.socket = socket;

//                //远端地址
//                IPEndPoint remote = socket.RemoteEndPoint as IPEndPoint;
//                remoteAddress = remote.Address.ToString();
//                remotePort = remote.Port;

//                //本地地址
//                IPEndPoint local = socket.LocalEndPoint as IPEndPoint;
//                localAddress = local.Address.ToString();
//                localPort = local.Port;
//            }
//        }


//        #region 公开方法
//        /// <summary>
//        /// 开始监听
//        /// </summary>
//        public bool BeginListen(int port, int queueSize = 1000)
//        {
//            localAddress = "127.0.0.1";
//            localPort = port;

//            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//            try
//            {
//                socket.Bind(new IPEndPoint(IPAddress.Any, localPort));
//                socket.Listen(queueSize);
//                socket.BeginAccept(acceptCallback, null);
//            }
//            catch (SocketException se)
//            {
//                if (onListen != null)
//                    onListen(2, se.ErrorCode, se.Message);

//                if (onError != null)
//                    onError(SocketState.BEGIN_LISTEN, se.ErrorCode, se.Message);
//            }
//            catch(Exception exp)
//            {
//                if (onListen != null)
//                    onListen(2, 0, exp.Message);

//                if (onError != null)
//                    onError(2, 0, exp.Message);
//            }
//        }

//        /// <summary>
//        /// 停止监听
//        /// </summary>
//        public void StopListen()
//        {

//        }

//        /// <summary>
//        /// 连接
//        /// </summary>
//        public void Connect()
//        {

//        }

//        /// <summary>
//        /// 断开连接
//        /// </summary>
//        public void Disconnect()
//        {

//        }

//        /// <summary>
//        /// 开始接受消息
//        /// </summary>
//        public void BeginReceive()
//        {

//        }

//        /// <summary>
//        /// 发送消息
//        /// </summary>
//        public void Send()
//        {

//        }
//        #endregion//公开方法

//        #region 回调函数

//        private void OnAccept(IAsyncResult iar) { }
//        private void OnConnect(IAsyncResult iar) { }
//        private void OnDisconnect(IAsyncResult iar) { }
//        private void OnSend(IAsyncResult iar) { }
//        private void OnReceive(IAsyncResult iar) { }

//        #endregion//回调函数


//    }

//}
