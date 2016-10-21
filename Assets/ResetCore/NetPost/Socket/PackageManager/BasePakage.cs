using UnityEngine;
using System.Collections;

namespace ResetCore.NetPost
{
    public abstract class BasePakage
    {
        //处理器名
        public abstract string handlerName { get; }

        //抱头长度
        public readonly int headLength = 4;

        //实际数据长度
        public int dataLength;

        //实际数据
        public byte[] data;

        //混合数据长度
        public int totalLength;

        //混合数据
        public byte[] totalData;

    }
}

