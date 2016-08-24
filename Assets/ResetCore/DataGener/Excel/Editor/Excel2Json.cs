using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using LitJson;
using System.IO;
using ResetCore.Json;

namespace ResetCore.Excel
{
    public class Excel2Json
    {
        public static void GenJson(ExcelReader excelReader, string outputPath = null)
        {

            ExcelReader exReader = excelReader;

            List<Dictionary<string, string>> rows = exReader.GetRows();

            JsonData data = new JsonData();
            string arrayString = JsonMapper.ToJson(rows);
            Debug.Log(arrayString);

            JsonData jsonArray = JsonMapper.ToObject(arrayString);
            data[excelReader.currentSheetName] = jsonArray;


            if (outputPath == null)
            {
                outputPath = PathConfig.GetLocalGameDataPath(PathConfig.DataType.Json)
                    + Path.GetFileNameWithoutExtension(excelReader.currentSheetName) + Data.GameDatas.Json.JsonData.m_fileExtention;
            }
            if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            }

            data.Save(outputPath);

            AssetDatabase.Refresh();
        }

        public static void GenCS(ExcelReader excelReader)
        {
            string className = excelReader.currentSheetName;
            DataClassesGener.CreateNewClass(className, typeof(Data.GameDatas.Json.JsonData), excelReader.fieldDict);
        }
    }

}
