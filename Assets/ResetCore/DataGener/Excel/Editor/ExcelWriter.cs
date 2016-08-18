using UnityEngine;
using System.Collections;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace ResetCore.Excel
{
    public class ExcelWriter
    {
        public string fileName { get; private set; }
        public string SheetName { get; private set; }
        public IWorkbook workbook { get; private set; }
        public ISheet sheet { get; private set; }

        public ExcelWriter(string fileName, string SheetName)
        {
            this.fileName = fileName;
            this.SheetName = SheetName;

            this.workbook = new XSSFWorkbook();
            this.sheet = workbook.CreateSheet(SheetName);
        }

        public void WriteLine(int lineNum, object[] obj)
        {
            //TODO
        }

        public void WriteRow(int rowNum, object[] obj)
        {
            //TODO
        }

        public void CreateFile()
        {
            FileStream fs = File.Create(Path.Combine(PathConfig.localGameDataExcelPath, fileName));
            workbook.Write(fs);
            fs.Close();
        }
    }

}
