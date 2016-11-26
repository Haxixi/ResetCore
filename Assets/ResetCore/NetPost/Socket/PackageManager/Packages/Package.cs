using UnityEngine;
using System.Collections;
using System;
using ResetCore.Util;
using ResetCore.Protobuf;

namespace ResetCore.NetPost
{
    /// <summary>
    /// 包结构为三段式
    /// -----------------------------
    /// 包头（长度信息）：4字节
    /// 包Id（HandlerId）：4字节
    /// 频道Id（ChannelId）：4字节
    /// 包内容（Protobuf）：剩余长度
    /// -----------------------------
    /// </summary>
    public class Package
    {
        //处理Id
        public int eventId { get; private set; }
       
        //实际数据长度
        public int dataLength { get; private  set; }

        //频道Id
        public int channelId { get; private set; }

        //抱头长度
        public static readonly int headLength = sizeof(int);

        //处理Id
        public static readonly int idLength = sizeof(int);

        //频道Id
        public static readonly int channelIdLength = sizeof(int);

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
        public static Package MakePakage<T>(int id, int channelId, T value, SendType sendType)
        {
            Package pkg = new Package();
            pkg.eventId = id;
            pkg.channelId = channelId;

            pkg.data = ProtoEx.Serialize<T>(value);

            pkg.dataLength = pkg.data.Length;
            pkg.totalLength = 
                pkg.dataLength + idLength + headLength + channelIdLength;

            byte[] lengthData = BitConverter.GetBytes(pkg.totalLength);

            if (BitConverter.IsLittleEndian && sendType == SendType.TCP)
            {
                Array.Reverse(lengthData);
            }

            byte[] eventIdData = BitConverter.GetBytes(pkg.eventId);
            byte[] channelIdData = BitConverter.GetBytes(pkg.channelId);

            pkg.totalData = lengthData.Concat(eventIdData).Concat(channelIdData).Concat(pkg.data);
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
            pkg.channelId = BitConverter.ToInt32(data.SubArray(headLength + idLength, channelIdLength), 0);
            pkg.data = data.SubArray(idLength + headLength + channelIdLength, data.Length - idLength - headLength - channelIdLength);

            return pkg;
        }

    }
}

