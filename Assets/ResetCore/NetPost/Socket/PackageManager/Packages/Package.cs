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
        public int dataLength { get; private  set; }

        //抱头长度
        public static readonly int headLength = sizeof(int);

        //处理Id
        public static readonly int idLength = sizeof(int);

        //实际数据
        public byte[] data { get; private set; }

        //混合数据长度
        public int totalLength { get; private set; }

        //混合数据
        public byte[] totalData { get; private set; }

        /// <summary>
        /// 包创建工厂
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Package MakePakage<T>(int id, T value)
        {
            Package pkg = new Package();
            pkg.eventId = id;
            pkg.data = ProtoEx.Serialize<T>(value);

            pkg.dataLength = pkg.data.Length;
            pkg.totalLength = pkg.dataLength + headLength;

            byte[] lengthData = BitConverter.GetBytes(pkg.totalLength);
            byte[] eventIdData = BitConverter.GetBytes(pkg.eventId);

            pkg.totalData = lengthData.Concat(eventIdData).Concat(pkg.data);
            return pkg;
        }

        //获取值
        public T GetValue<T>()
        {
            return ProtoEx.DeSerialize<T>(data);
        }
        

        /// <summary>
        /// 解析包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Package PrasePackage(byte[] data)
        {
            Package pkg = new Package();
            pkg.totalData = data;
            pkg.dataLength = BitConverter.ToInt32(data.SubArray(0, headLength), 0);
            pkg.eventId = BitConverter.ToInt32(data.SubArray(headLength, idLength), 0);
            pkg.data = data.SubArray(8, data.Length - idLength - headLength);

            return pkg;
        }

        /// <summary>
        /// 获取包长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            var lengthBytes = totalData.SubArray(0, 4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);
            
            return BitConverter.ToInt32(lengthBytes, 0);
        }
    }
}

