using UnityEngine;
using System.Collections;
using System;
using ResetCore.Util;
using ResetCore.Protobuf;

namespace ResetCore.NetPost
{
    public class Package
    {
        //处理Id
        public int eventId { get; private set; }
       
        //实际数据长度
        public int dataLength;

        //抱头长度
        public readonly int headLength = sizeof(int);

        //处理Id
        public readonly int idLength = sizeof(int);

        //实际数据
        public byte[] data { get; private set; }

        //混合数据长度
        public int totalLength { get; private set; }

        //混合数据
        public byte[] totalData { get; private set; }


        public void MakePakage<T>(T value)
        {
            data = ProtoEx.Serialize<T>(value);
            dataLength = data.Length;
            totalLength = dataLength + headLength;

            byte[] lengthData = BitConverter.GetBytes(totalLength);
            byte[] eventIdData = BitConverter.GetBytes(eventId);

            totalData = lengthData.Concat(eventIdData).Concat(data);
        }

        public T GetValue<T>()
        {
            return ProtoEx.DeSerialize<T>(data);
        }
        

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

