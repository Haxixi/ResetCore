using UnityEngine;
using System.Collections;
using LitJson;
using ResetCore.Util;

namespace ResetCore.Json
{
    public static class JsonDataEx
    {

        public static void Save(this JsonData data, string path)
        {
            FileEx.SaveText(data.ToJson(), path);
        }


    }
}
