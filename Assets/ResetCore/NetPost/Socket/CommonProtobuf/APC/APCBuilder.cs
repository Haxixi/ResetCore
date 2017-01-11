using System.Collections;
using Protobuf.Data;
using ResetCore.Util;

namespace ResetCore.NetPost
{
    public class APCBuilder
    {
        /// <summary>
        /// 构建APCData
        /// </summary>
        /// <param name="id"></param>
        /// <param name="functionName"></param>
        /// <param name="args"></param>
        public static APCData BuildAPC(int id, string functionName, ArrayList args)
        {
            APCData data = new APCData();
            data.Id = id;
            APCBean bean = new APCBean(functionName, args);
            data.Content = ByteConvertHelper.Object2Bytes(bean);
            return data;
        }
    }
}
