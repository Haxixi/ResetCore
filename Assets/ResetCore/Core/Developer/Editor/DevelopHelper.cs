#if RESET_DEVELOPER

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using ResetCore.Asset;

public static class DevelopHelper {

    
    //将在工程目录下写好的ExtraTool打包至ResetCore根目录下
    [MenuItem("Tools/DeveloperTools/Compress Extra Tools")]
    public static void ExportExtraToolsToPackage()
    {
        if (File.Exists(PathConfig.ExtraToolPathInPackage))
        {
            File.Delete(PathConfig.ExtraToolPathInPackage);
        }
        CompressHelper.CompressDirectory(PathConfig.ExtraToolPath, PathConfig.ExtraToolPathInPackage);
        Debug.logger.Log("Compress To " + PathConfig.ExtraToolPathInPackage);
    }

    [MenuItem("Tools/DeveloperTools/Compress SDK")]
    public static void ExportSDKToPackage()
    {
        if (File.Exists(PathConfig.SDKPathInPackage))
        {
            File.Delete(PathConfig.SDKPathInPackage);
        }
        CompressHelper.CompressDirectory(PathConfig.SDKBackupPath, PathConfig.SDKPathInPackage);
        Debug.logger.Log("Compress To " + PathConfig.SDKBackupPath);
    }

    [MenuItem("Tools/DeveloperTools/Open Todo List")]
    public static void OpenTodoList()
    {
        EditorUtility.OpenWithDefaultApp(PathConfig.ResetCorePath + "TodoList.txt");
    }
}

#endif