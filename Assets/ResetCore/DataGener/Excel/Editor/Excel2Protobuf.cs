using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ResetCore.CodeDom;
using ResetCore.Util;
using ResetCore.Data;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using ResetCore.Data.GameDatas.Protobuf;

namespace ResetCore.Excel
{
    public class Excel2Protobuf
    {
        public static void GenProtobuf(ExcelReader excelReader, string outputPath = null)
        {
            string className = excelReader.currentSheetName;
            Type protobufDataType = Type.GetType(ProtobufData.nameSpace + "." + className + ",Assembly-CSharp");
            //Debug.Log(ProtobufData.nameSpace + "." + className + ",Assembly-CSharp" + " 是否存在" + protobufDataType.ToString());

            if (protobufDataType == null)
            {
                GenCS(excelReader);
                protobufDataType = Type.GetType(ProtobufData.nameSpace + "." + className + ",Assembly-CSharp");
                Debug.logger.Log("Gen the CS File Please");
            }

            List<Dictionary<string, object>> rowObjs = excelReader.GetRowObjs();

            List<object> result = new List<object>();
            for (int i = 0; i < rowObjs.Count; i++)
            {
                object item = Activator.CreateInstance(protobufDataType);
                PropertyInfo[] propertys = protobufDataType.GetProperties();
                foreach (KeyValuePair<string, object> pair in rowObjs[i])
                {
                    PropertyInfo prop = propertys.First((pro) => { return pro.Name == pair.Key; });
                    prop.SetValue(item, pair.Value, null);
                    Debug.logger.Log(pair.ConverToString());
                }

                result.Add(item);
            }

            string protoPath = PathConfig.GetLocalGameDataPath(PathConfig.DataType.Protobuf);
            PathEx.MakeDirectoryExist(protoPath);

            if (ProtoBuf.Serializer.NonGeneric.CanSerialize(protobufDataType))
            {
                string resPath = protoPath + className + ProtobufData.ex;
                if(outputPath != null)
                {
                    resPath = outputPath;
                }
                string root = Path.GetDirectoryName(resPath);
                PathEx.MakeDirectoryExist(root);
                using (var file = System.IO.File.Create(resPath))
                {
                    ProtoBuf.Serializer.NonGeneric.Serialize(file, result);
                    Debug.logger.Log(resPath + "导出成功");
                }

            }
            else
            {
                Debug.logger.LogError("序列化", protobufDataType.FullName + "不可序列化！");
            }



        }

        public static void GenCS(ExcelReader excelReader)
        {


            string className = excelReader.currentSheetName;

            CodeGener protobufBaseGener = new CodeGener(ProtobufData.nameSpace, className);
            protobufBaseGener.newClass.AddMemberCostomAttribute("ProtoBuf.ProtoContract");
            protobufBaseGener.AddImport("System", "ProtoBuf");

            List<string> comment = excelReader.GetComment();

            protobufBaseGener
                .AddBaseType("ProtobufData<" + className + ">")
                .AddMemberField(typeof(string), "fileName", (member) =>
                {
                    member.AddFieldMemberInit("\"" + className + "\"");
                }, System.CodeDom.MemberAttributes.Static | System.CodeDom.MemberAttributes.Final | System.CodeDom.MemberAttributes.Public);

            excelReader.GetTitle().ForEach((i, title) =>
            {
                string[] titleSplit = title.Split('|');
                string varName = titleSplit[0];
                string typeName = titleSplit[1];
                protobufBaseGener.AddMemberProperty(typeName.GetTypeByString(), varName, (member) =>
                {
                    member.AddComment(comment[i], true);
                    member.AddMemberCostomAttribute("ProtoBuf.ProtoMember", (i+1).ToString());
                });
            });
            PathEx.MakeDirectoryExist(PathConfig.GetLoaclGameDataClassPath(PathConfig.DataType.Protobuf));
            protobufBaseGener.GenCSharp(PathConfig.GetLoaclGameDataClassPath(PathConfig.DataType.Protobuf));

            
        }

    }

}
