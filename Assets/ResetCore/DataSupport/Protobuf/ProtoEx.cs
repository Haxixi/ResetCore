using UnityEngine;
using System.Collections;
using System.IO;

namespace ResetCore.Protobuf
{
    public class ProtoEx
    {
        /// <summary>
        /// 从Bytes中读取类 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Read<T>(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            return ProtoBuf.Serializer.Deserialize<T>(ms);
        }

        /// <summary>
        /// 将类写入到路径下的文件中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void Write(object obj, string path)
        {
            using (var file = System.IO.File.Create(path))
            {
                ProtoBuf.Serializer.NonGeneric.Serialize(file, obj);
            }
        }

    }

}
