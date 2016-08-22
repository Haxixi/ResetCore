using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor;
using ResetCore.Data.GameDatas.Xml;

namespace ResetCore.Excel
{
    public class Excel2Xml
    {
        public static void GenXml(ExcelReader excelReader, string outputPath = null)
        {

            ExcelReader exReader = excelReader;

            XDocument xDoc = new XDocument();
            XElement root = new XElement("Root");
            xDoc.Add(root);

            List<Dictionary<string, string>> rows = exReader.GetRows();
            for (int i = 0; i < rows.Count; i++)
            {
                XElement item = new XElement("item");
                root.Add(item);
                foreach (KeyValuePair<string, string> pair in rows[i])
                {
                    item.Add(new XElement(pair.Key, pair.Value));
                }

            }

            if (outputPath == null)
            {
                outputPath = PathConfig.GetLocalGameDataPath(PathConfig.DataType.Xml) 
                    + Path.GetFileNameWithoutExtension(excelReader.currentSheetName) + XmlData.m_fileExtention;
            }
            if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            }

            xDoc.Save(outputPath);
            AssetDatabase.Refresh();
        }

        public static void GenCS(ExcelReader excelReader)
        {
            string className = excelReader.currentSheetName;
            DataClassesGener.CreateNewClass(className, typeof(XmlData), excelReader.fieldDict);
        }
       
    }

}
