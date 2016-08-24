using UnityEngine;
using System.Collections;
using System.IO;
using ResetCore.Data.GameDatas.Obj;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using ResetCore.Util;
using UnityEditor;

namespace ResetCore.Excel
{
    public class Excel2ScrObj
    {

        public static void GenObj(ExcelReader excelReader, string outputPath = null)
        {
            string className = excelReader.currentSheetName;

            Type objDataType = Type.GetType(ObjData.nameSpace + "." + className + ",Assembly-CSharp");
            ObjDataBundle data = ScriptableObject.CreateInstance<ObjDataBundle>();

            List<Dictionary<string, object>> rowObjs = excelReader.GetRowObjs();

            ArrayList result = new ArrayList();
            for (int i = 0; i < rowObjs.Count; i++)
            {
                var item = Activator.CreateInstance(objDataType);
                PropertyInfo[] propertys = objDataType.GetProperties();
                foreach (KeyValuePair<string, object> pair in rowObjs[i])
                {
                    Debug.logger.Log(pair.ConverToString());
                    PropertyInfo prop = propertys.First((pro) => { return pro.Name == pair.Key; });
                    prop.SetValue(item, pair.Value, null);
                }

                data.dataArray.Add(item);
            }

            string dataPath = PathConfig.GetLocalGameDataPath(PathConfig.DataType.Obj);
            PathEx.MakeDirectoryExist(dataPath);

            
            string resPath = dataPath + className + ObjData.ex;
            if (outputPath != null)
            {
                resPath = outputPath;
            }
            string root = Path.GetDirectoryName(resPath);
            PathEx.MakeDirectoryExist(root);

            AssetDatabase.CreateAsset(data, "Assets/"+ className + ObjData.ex);

            string tempPath = Path.Combine(Application.dataPath,  className + ObjData.ex);
            if (File.Exists(resPath))
            {
                File.Delete(resPath);
            }
            File.Move(tempPath, resPath);
            Debug.Log("保存到了" + resPath);

        }

        public static void GenCS(ExcelReader excelReader)
        {
            string className = excelReader.currentSheetName;
            DataClassesGener.CreateNewClass(className, typeof(ObjData), excelReader.fieldDict);
        }
    }

}
