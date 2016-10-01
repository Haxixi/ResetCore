using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ResetCore.Data
{
    public interface IDataReadable
    {
        string currentDataTypeName { get; }
        Dictionary<string, Type> fieldDict { get; }
        string filepath { get; }
        bool IsValid();
        List<string> GetTitle(int start = 0);
        List<string> GetMemberNames(int start = 0);
        List<Type> GetMemberTypes(int start = 0);
        List<string> GetComment(int start = 1);
        string[] GetSheetNames();
        List<Dictionary<string, string>> GetRows(int start = 2);
        List<string> GetRow(int rowNum, int startLine = 0, int endLine = -1);
        List<string> GetLine(int lineNum, int startRow = 0, int endRow = -1);
        List<Dictionary<string, object>> GetRowObjs(int start = 2);
    }

}
