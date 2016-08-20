using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using ResetCore.Data;
using System.IO;
using ResetCore.Data.GameDatas.Xml;
using UnityEditor;

namespace ResetCore.Excel
{
    public class Excel2Localization
    {
        public static void ExportExcelFile()
        {
            GenXml();

        }

        private static void GenXml()
        {
            ExcelReader excelReader = new ExcelReader(PathConfig.LanguageDataExcelPath);

            XDocument xDoc = new XDocument();
            XElement root = new XElement("Root");
            xDoc.Add(root);

            List<string> commentLine = excelReader.GetLine(0, 1);
            List<string> keyLine = excelReader.GetLine(1, 1);

            int num = 2;
            Array languageType = Enum.GetValues(typeof(LanguageConst.LanguageType));
            foreach (LanguageConst.LanguageType type in languageType)
            {
                XElement languageEle = new XElement("item");
                root.Add(languageEle);

                List<string> valueLine = excelReader.GetLine(num, 1, keyLine.Count);

                for (int i = 0; i < keyLine.Count; i++)
                {
                    if (valueLine.Count > i)
                    {
                        languageEle.Add(new XElement(keyLine[i], valueLine[i]));
                    }
                    else
                    {
                        languageEle.Add(new XElement(keyLine[i], valueLine[i]));
                    }
                }

                num++;
            }

            string outputPath = PathConfig.LanguageDataPath;
            if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            }

            xDoc.Save(outputPath);

            //生成代码文件用于读取
            //string className = excelReader.currentSheetName;

            //Dictionary<string, Type> fieldDict = new Dictionary<string,Type>();
            //int row = 1;
            //foreach (string key in keyLine)
            //{
            //    if (fieldDict.ContainsKey(key))
            //    {
            //        Debug.logger.LogError("Gen Localization Data", "Same Key: " + key + " Row Number: " + row + " has been in the data");
            //        continue;
            //    }
            //    fieldDict.Add(key, typeof(string));
            //    row++;
            //}

            //DataClassesGener.CreateNewClass(className, typeof(XmlData), fieldDict, PathConfig.localLanguageDataClassPath);

            AssetDatabase.Refresh();
        }

    }

}
