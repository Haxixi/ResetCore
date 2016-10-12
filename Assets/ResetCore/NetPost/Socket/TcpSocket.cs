using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Net;
using ResetCore.Event;

namespace ResetCore.NetPost
{
    public class TcpSocket
    {
        /// <summary>
        ///远端端口
        /// </summary>
        public int remotePort { get; private set; }
        /// <summary>
        /// 远端地址
        /// </summary>
        public string remoteAddress { get; private set; }
        /// <summary>
        /// 本地端口
        /// </summary>
        public int localPort { get; private set; }
        /// <summary>
        /// 本地地址
        /// </summary>
        public string localAddress { get; private set; }
        /// <summary>
        /// Socket变量
        /// </summary>
        private Socket socket;

        /// <summary>
        /// 是否为主动关闭
        /// </summary>
        private bool isCloseSelf = false;

        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] receiveBuffer = new byte[1024 * 5];

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool isConneted
        {
            get
            {
                if (socket == null || !socket.Connected)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #region 公开的回调函数

        public TcpSocketListenDelegate onListen { get; private set; }
        public TcpSocketConnectDelegate onConnect { get; private set; }
        public TcpSocketDisconnectDelegate onDisconnect { get; private set; }
        public TcpSocketErrorDelegate onError { get; private set; }
        public TcpSocketReceiveDelegate onReveive { get; private set; }
        public TcpSocketSendDelegate onSend { get; private set; }

        #endregion

        public TcpSocket(Socket socket = null)
        {
            acceptCallback = new AsyncCallback(OnAccept);
            connectCallback = new AsyncCallback(OnConnect);
            disconnectCallback = new AsyncCallback(OnDisconnect);
            sendCallback = new AsyncCallback(OnSend);
            receiveCallback = new AsyncCallback(OnReceive);

            if (socket != null)
            {
                this.socket = socket;

                //远端地址
                IPEndPoint remote = socket.RemoteEndPoint as IPEndPoint;
                remoteAddress = remote.Address.ToString();
                remotePort = remote.Port;

                //本地地址
                IPEndPoint local = socket.LocalEndPoint as IPEndPoint;
                localAddress = local.Address.ToString();
                localPort = local.Port;
            }
        }


        #region 公开方法用于执行各种Socket行为
        /// <summary>
        /// 开始监听等待接收到新信息的回调
        /// </summary>
        public bool BeginListen(int port, int queueSize = 1000)
        {
            localAddress = "127.0.0.1";
            localPort = port;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, localPort));
                socket.Listen(queueSize);
                socket.BeginAccept(acceptCallback, null);
            }
            catch (SocketException se)
            {
                if (onListen != null)
                    onListen(2, se.ErrorCode, se.Message);

                if (onError != null)
                    onError(SocketState.BEGIN_LISTEN, se.ErrorCode, se.Message);

                return false;
            }
            catch (Exception exp)
            {
                if (onListen != null)
                    onListen(2, 0, exp.Message);

                if (onError != null)
                    onError(SocketState.BEGIN_LISTEN, 0, exp.Message);

                return false;
            }

            if (onListen != null)
                onListen(0, 0, "");

            return true;
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void StopListen()
        {
            //设置主动关闭标志位
            isCloseSelf = true;

            try
            {
                //停止监听
                if (socket != null)
                    socket.Close();
            }
            catch (SocketException se)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.END_LISTEN, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.END_LISTEN, 0, exp.Message);
            }
        }

        /// <summary>
        /// 连接等待成功连接的回调
        /// </summary>
        public void Connect(string address, int port)
        {
            remoteAddress = address;
            remotePort = port;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            isCloseSelf = false;

            try
            {
                //尝试连接
                socket.BeginConnect(remoteAddress, remotePort, connectCallback, null);
            }
            catch (SocketException se)
            {
                if (onConnect != null)
                    onConnect(1, se.ErrorCode, se.Message);

                if (onError != null)
                    onError(SocketState.BEGIN_CONNECT, se.ErrorCode, se.Message);
            }
            catch(Exception e)
            {
                if (onConnect != null)
                    onConnect(2, 0, e.Message);

                if (onError != null)
                    onError(SocketState.BEGIN_CONNECT, 0, e.Message);
            }
        }

        /// <summary>
        /// 断开连接等待断开成功的回调
        /// </summary>
        public void Disconnect()
        {
            //判断是否已连接
            if (!isConneted)
                return;

            //设置主动关闭位
            isCloseSelf = true;

            try
            {
                //开始断开连接
                socket.BeginDisconnect(false, disconnectCallback, null);
            }
            catch (SocketException se)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.BEGIN_DISCONNECT, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.BEGIN_DISCONNECT, 0, exp.Message);
            }
        }

        /// <summary>
        /// 开始接受消息等待回调
        /// </summary>
        public void BeginReceive()
        {
            try
            {
                //开始接收数据
                socket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, 0, receiveCallback, null);
            }
            catch (SocketException se)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.BEGIN_RECEIVE, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.BEGIN_RECEIVE, 0, exp.Message);
            }
        }

        /// <summary>
        /// 发送消息并等待发送成功的回调
        /// </summary>
        public void Send(byte[] data)
        {
            try
            {
                //开始接收数据
                socket.BeginSend(data, 0, data.Length, 0, sendCallback, null);
            }
            catch (SocketException se)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.BEGIN_SEND, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发报错事件
                if (onError != null)
                    onError(SocketState.BEGIN_SEND, 0, exp.Message);
            }
        }
        #endregion//公开方法

        #region 回调函数
        /// <summary>
        /// 当接收完成时回调
        /// </summary>
        private AsyncCallback acceptCallback;
        private void OnAccept(IAsyncResult iar)
        {
            Socket client;

            try
            {
                //获取客户端套接字
                client = socket.EndAccept(iar);
                //继续等待连接
                socket.BeginAccept(acceptCallback, null);
            }
            catch(SocketException se)
            {
                if (onError != null)
                    onError(SocketState.ACCEPT, se.ErrorCode, se.Message);
                return;
            }
            catch(Exception e)
            {
                if (onError != null)
                    onError(SocketState.ACCEPT, 0, e.Message);
            }

            if (onConnect != null)
                onConnect(0, 0, "");


        }
        /// <summary>
        /// 当连接完成时回调
        /// </summary>
        private AsyncCallback connectCallback;
        private void OnConnect(IAsyncResult iar)
        {
            try
            {
                //结束连接
                socket.EndConnect(iar);

                IPEndPoint local = socket.LocalEndPoint as IPEndPoint;
                localAddress = local.Address.ToString();
                localPort = local.Port;
            }
            catch(SocketException se)
            {

            }
        }
        /// <summary>
        /// 当断开连接时回调
        /// </summary>
        private AsyncCallback disconnectCallback;
        private void OnDisconnect(IAsyncResult iar) { }
        /// <summary>
        /// 当发送完成时回调
        /// </summary>
        private AsyncCallback sendCallback;
        private void OnSend(IAsyncResult iar) { }
        /// <summary>
        /// 当接收信息时回调
        /// </summary>
        private AsyncCallback receiveCallback;
        private void OnReceive(IAsyncResult iar) { }


        #endregion//回调函数


    }

}
