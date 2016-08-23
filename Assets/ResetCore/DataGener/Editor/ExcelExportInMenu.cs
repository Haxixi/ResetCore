using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.IO;
using ResetCore.Data;
using System;

namespace ResetCore.Excel
{
    public class ExcelExportInMenu
    {
        /// <summary>
        /// 导出选中的Excel为Xml
        /// </summary>
        [MenuItem("Assets/DataHelper/Xml/Export Selected Excel")]
        public static void ExportAllSelectedExcelToXml()
        {
            ExportData((item, sheetName) =>
            {
                ExcelReader excelReader = new ExcelReader(item, sheetName);
                Excel2Xml.GenXml(excelReader);
                Excel2Xml.GenCS(excelReader);
            });
        }

        [MenuItem("Assets/DataHelper/Json/Export Selected Excel")]
        public static void ExportAllSelectedExcelToJson()
        {
            ExportData((item, sheetName) =>
            {
                ExcelReader excelReader = new ExcelReader(item, sheetName);
                Excel2Json.GenJson(excelReader);
                Excel2Json.GenCS(excelReader);
            });
        }

        /// <summary>
        /// 导出选中的Excel为Protobuf（暂时不可用）
        /// </summary>
        //[MenuItem("Assets/DataHelper/Protobuf/Export Selected Excel")]
        //public static void ExportAllSelectedExcelToProtobuf()
        //{
        //    var selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        //    var paths = (from s in selection
        //                 let path = AssetDatabase.GetAssetPath(s)
        //                 where (path.EndsWith(".xlsx") || path.EndsWith(".xls"))
        //                 select path).ToArray();

        //    int num = 1;
        //    foreach (string item in paths)
        //    {
        //        Debug.Log(item);
        //        ExcelReader excelReader = new ExcelReader(item);
        //        foreach (string sheetName in excelReader.GetSheetNames())
        //        {
        //            EditorUtility.DisplayProgressBar
        //                ("Exporting Excel", "Current: " + num + "/" + paths.Length + " File: " + Path.GetFileName(item) +
        //                " Sheet: " + sheetName, (float)num / (float)paths.Length);

        //            excelReader = new ExcelReader(item, sheetName);
        //            Excel2Protobuf.GenCS(excelReader);
        //            Excel2Protobuf.GenProtobuf(excelReader);
        //        }
        //        num++;
        //    }
        //    EditorUtility.ClearProgressBar();
        //    Debug.logger.Log("Finished");
        //}

        /// <summary>
        /// 导出选中的Excel为首选项
        /// </summary>
        [MenuItem("Assets/DataHelper/PrefData/Export Selected Excel")]
        public static void ExportAllSelectedExcelToPrefData()
        {
            ExportData((item, sheetName) =>
            {
                ExcelReader excelReader = new ExcelReader(item, sheetName, ExcelType.Pref);
                Excel2PrefData.GenPref(excelReader);
                Excel2PrefData.GenCS(excelReader);
            });
        }

        

        /// <summary>
        /// 导出所有本地化数据
        /// </summary>
        [MenuItem("Assets/DataHelper/Language/Export All Language File")]
        [MenuItem("Tools/GameData/Language/Export All Language File")]
        static void ExportLanguageFile()
        {
            Excel2Localization.ExportExcelFile();
        }


        private static void ExportData(Action<string, string> genAction)
        {
            var selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            var paths = (from s in selection
                         let path = AssetDatabase.GetAssetPath(s)
                         where (path.EndsWith(".xlsx") || path.EndsWith(".xls"))
                         select path).ToArray();

            int num = 1;
            foreach (string item in paths)
            {
                ExcelReader excelReader = new ExcelReader(item);
                foreach (string sheetName in excelReader.GetSheetNames())
                {
                    EditorUtility.DisplayProgressBar
                        ("Exporting Excel", "Current: " + num + "/" + paths.Length + " File: " + Path.GetFileName(item) +
                        " Sheet: " + sheetName, (float)num / (float)paths.Length);

                    genAction(item, sheetName);

                }
                num++;
            }
            EditorUtility.ClearProgressBar();
            Debug.logger.Log("Finished");
        }
        
    }

}
