using UnityEngine;
using System.Collections;
using System;

namespace ResetCore.NetPost
{
    public class PackageReciver
    {
        /// <summary>
        /// 数据缓冲区
        /// </summary>
        private byte[] packetBuffer = new byte[0];

        public void ReceivePackage(int len, byte[] data)
        {
            packetBuffer.Concat(data);

            //标记是否有完整的包接收到
            bool hasCompletePacket = false;
            do
            {
                //Todo
            }while(hasCompletePacket)
        }

        private int GetDataLength(byte[] data)
        {
            if (data.Length < Package.headLength)
                return 0;

            byte[] lengthByte = data.SubArray(0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthByte);
            int length = BitConverter.ToInt32(lengthByte, 0);

            return length;
        }
    }
}
