#if MYSQL
using UnityEngine;
using System.Collections;
using ResetCore.Data;
using System;
using System.Collections.Generic;
using ResetCore.MySQL;

public class SQLReader : IDataReadable
{
    /// <summary>
    /// 数据库名
    /// </summary>
    public string database { get; private set; }
    /// <summary>
    /// 账号
    /// </summary>
    public string id { get; private set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string pwd { get; private set; }
    /// <summary>
    /// 服务器ip
    /// </summary>
    public string host { get; private set; }
    /// <summary>
    /// 接口名
    /// </summary>
    public string port { get; private set; }

    public string currentDataTypeName
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public Dictionary<string, Type> fieldDict
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string filepath
    {
        get
        {
            return host;
        }
    }

    public SQLReader(string database, string id = "root", string pwd = "123456", string host = "127.0.0.1", string port = "3306")
    {
        this.database = database;
        this.id = id;
        this.pwd = pwd;
        this.host = host;
        this.port = port;

        MySQLManager.OpenSql(host, database, id, pwd, port);
    }

    public List<string> GetComment(int start = 1)
    {
        throw new NotImplementedException();
    }

    public List<string> GetLine(int lineNum, int startRow = 0, int endRow = -1)
    {
        throw new NotImplementedException();
    }

    public List<string> GetMemberNames(int start = 0)
    {
        throw new NotImplementedException();
    }

    public List<Type> GetMemberTypes(int start = 0)
    {
        throw new NotImplementedException();
    }

    public List<string> GetRow(int rowNum, int startLine = 0, int endLine = -1)
    {
        throw new NotImplementedException();
    }

    public List<Dictionary<string, object>> GetRowObjs(int start = 2)
    {
        throw new NotImplementedException();
    }

    public List<Dictionary<string, string>> GetRows(int start = 2)
    {
        throw new NotImplementedException();
    }

    public string[] GetSheetNames()
    {
        throw new NotImplementedException();
    }

    public List<string> GetTitle(int start = 0)
    {
        throw new NotImplementedException();
    }

    public bool IsValid()
    {
        return MySQLManager.isOpen;
    }
}
#endif