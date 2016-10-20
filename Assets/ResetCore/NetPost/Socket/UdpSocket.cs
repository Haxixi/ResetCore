using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Net;

namespace ResetCore.NetPost
{
    public class UdpSocket
    {
        public string remoteAddress { get; private set; }
        public int remotePort { get; private set; }
        public string localAddress { get; private set; }
        public int localPort { get; private set; }

        private Socket socket;

        private AsyncCallback receiveCallback;
        private AsyncCallback sendCallback;

        //各类回调事件回调
        public UdpSocketListenDelegate onListen;
        public UdpSocketBindDelegate onBind;
        public UdpSocketReceiveDelegate onReceive;
        public UdpSocketErrorDelegate onError;

        //接收缓存
        private byte[] receiveBuffer = new byte[1024 * 1024];

        //广播地址
        private EndPoint endpointAny = new IPEndPoint(IPAddress.Any, 0);
        //远端地址
        private EndPoint remoteEndpoint;

        public UdpSocket()
        {
            receiveCallback = new AsyncCallback(OnReceive);
            sendCallback = new AsyncCallback(OnSend);
        }

        //开始监听
        public bool StartListen(int localPt)
        {
            localPort = localPt;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(new IPEndPoint(IPAddress.Any, localPort));
                socket.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, 0, ref endpointAny, receiveCallback, null);
            }
            catch (SocketException se)
            {
                //引发监听失败事件
                if (onListen != null)
                    onListen(1, se.ErrorCode, se.Message);

                return false;
            }
            catch (Exception exp)
            {
                //引发监听失败事件
                if (onListen != null)
                    onListen(2, 0, exp.Message);

                return false;
            }

            //引发监听成功事件
            if (onListen != null)
                onListen(0, 0, "");

            return true;

        }

        //绑定到本地接口
        public bool BindLocalEndPoint(int localPt)
        {
            try
            {
                //绑定本地端口
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(new IPEndPoint(IPAddress.Any, localPt));

                //读取本地地址和端口
                IPEndPoint local = socket.LocalEndPoint as IPEndPoint;
                localAddress = local.Address.ToString();
                localPort = local.Port;
            }
            catch (SocketException se)
            {
                //引发绑定失败事件
                if (onBind != null)
                    onBind(1, se.ErrorCode, se.Message);

                return false;
            }
            catch (Exception exp)
            {
                //引发绑定失败事件
                if (onBind != null)
                    onBind(2, 0, exp.Message);

                return false;
            }

            //引发绑定成功事件
            if (onBind != null)
                onBind(0, 0, "");

            return true;
        }

        //绑定到远端地址
        public bool BindRemoteEndPoint(string remoteAddr, int remotePt, 
            int localPt, bool autoRebind = true)
        {
            //保存地址
            SaveRemoteAddress(remoteAddr, remotePt);
            localPort = localPt;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                //判断是否绑定本地端口
                if (localPort != 0)
                {
                    if (autoRebind)
                        RebindLocalPort(localPt);
                    else
                    {
                        //绑定本地端口
                        socket.Bind(new IPEndPoint(IPAddress.Any, localPt));
                    }

                    //读取本地地址和端口
                    IPEndPoint local = socket.LocalEndPoint as IPEndPoint;
                    localAddress = local.Address.ToString();
                    localPort = local.Port;
                }
            }
            catch (SocketException se)
            {
                //引发绑定失败事件
                if (onBind != null)
                    onBind(1, se.ErrorCode, se.Message);

                return false;
            }
            catch (Exception exp)
            {
                //引发绑定失败事件
                if (onBind != null)
                    onBind(1, 0, exp.Message);

                return false;
            }

            //引发绑定成功事件
            if (onBind != null)
                onBind(0, 0, "");

            return true;
        }

        //停止连接
        public void Stop()
        {
            try
            {
                //关闭套接字
                socket.Close();
            }
            catch (SocketException se)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.CLOSE, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.CLOSE, 0, exp.Message);
            }
        }

        //开始接收
        public bool BeginReceive()
        {
            try
            {
                //开始接收
                socket.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, 0, ref endpointAny, receiveCallback, null);

                return true;
            }
            catch (SocketException se)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.BEGIN_RECEIVE, se.ErrorCode, se.Message);

                return false;
            }
            catch (Exception exp)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.BEGIN_RECEIVE, 0, exp.Message);

                return false;
            }
        }

        //向远端发送消息
        public void Send(byte[] data, int len)
        {
            try
            {
                //开始发送
                socket.BeginSendTo(data, 0, len, 0, remoteEndpoint, sendCallback, null);
            }
            catch (SocketException se)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.BEGIN_SEND, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.BEGIN_SEND, 0, exp.Message);
            }
        }

        //向远端发送消息
        public void Send(byte[] data, int len, string remoteAddr, int remotePt)
        {
            //保存远端地址
            SaveRemoteAddress(remoteAddr, remotePt);

            try
            {
                //开始发送
                socket.BeginSendTo(data, 0, len, 0, remoteEndpoint, sendCallback, null);
            }
            catch (SocketException se)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.BEGIN_SEND, se.ErrorCode, se.Message);
            }
            catch (Exception exp)
            {
                //引发错误事件
                if (onError != null)
                    onError(SocketState.BEGIN_SEND, 0, exp.Message);
            }
        }

        //重新绑定接口
        private void RebindLocalPort(int pt)
        {
            int port = pt;

            bool success = false;
            while (!success)
            {
                try
                {
                    socket.Bind(new IPEndPoint(IPAddress.Any, port++));
                    success = true;
                }
                catch { }
            }
        }

        private void OnReceive(IAsyncResult iar) { }

        private void OnSend(IAsyncResult iar) { }

        //保存远端地址
        private void SaveRemoteAddress(string address, int port)
        {
            //保存地址
            remoteAddress = address;
            remotePort = port;

            //生成端点
            remoteEndpoint = new IPEndPoint(IPAddress.Parse(remoteAddress), remotePort);
        }
    }
}
