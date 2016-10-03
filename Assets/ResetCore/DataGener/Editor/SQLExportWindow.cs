using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ResetCore.Data
{
    public class SQLExportWindow : EditorWindow
    {

        //显示窗口的函数
        [MenuItem("Tools/GameData/MySQL Exporter")]
        static void ShowMainWindow()
        {
            SQLExportWindow window =
                EditorWindow.GetWindow(typeof(SQLExportWindow), true, "Excel Exporter") as SQLExportWindow;
            window.Show();
        }
    }

}
