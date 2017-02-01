using UnityEngine;
using System.Collections;

namespace ResetCore.NetPost
{
    public static class ServerConst
    {

        //http服务器响应url
        public const string HttpNetPostURL = "127.0.0.1:8000";
        //Tcp/Udp服务器地址
        public static readonly string ServerAddress = "127.0.0.1";
        public static readonly int TcpRemotePort = 9000;
        public static readonly int UdpRemotePort = 9051;
        public static readonly int UdpLocalPort = 10000;
    }
}
