using UnityEngine;
using System.Collections;
using System;

namespace ResetCore.NetPost
{
    public abstract class BasePakage
    {
        //处理器名
        public abstract string handlerName { get; }

        //抱头长度
        public readonly int headLength = sizeof(int);

        //实际数据长度
        public int dataLength;

        //实际数据
        public byte[] data;

        //混合数据长度
        public int totalLength;

        //混合数据
        public byte[] totalData;

        /// <summary>
        /// 获取包长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            var lengthBytes = totalData.SubArray(0, 0, 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);
            
            return BitConverter.ToInt32(lengthBytes, 0);
        }
    }
}

