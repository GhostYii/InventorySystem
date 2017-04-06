using System;
using System.IO;
using UnityEngine;

public enum LogType
{
    Error,
    Warning,
    Mes,
    Tip,
    Other
}

/// <summary>
/// 将某些信息输出至文本
/// </summary>
public static class DebugMes
{
    /// <summary>
    /// 输出日志信息
    /// </summary>
    /// <param name="mes">需要输出的信息</param>
    public static void Log(string mes)
    {
        using (TextWriter tw = File.AppendText(Application.dataPath + "/Log.txt"))
        {
            tw.WriteLine(TypeHead(LogType.Mes) + mes + "\t" + DateTime.Now.ToString());
            tw.Close();
        }
    }

    /// <summary>
    /// 输出日志信息
    /// </summary>
    /// <param name="mes">需要输出的信息</param>
    /// <param name="showHead">是否显示信息标头</param>
    /// <param name="showTime">是否显示输出时间</param>
    public static void Log(string mes, bool showHead, bool showTime)
    {
        using (TextWriter tw = File.AppendText(Application.dataPath + "/Log.txt"))
        {
            if (showHead)
                if (showTime)
                    tw.WriteLine(TypeHead(LogType.Mes) + mes + "\t" + DateTime.Now.ToString());
                else
                    tw.WriteLine(TypeHead(LogType.Mes) + mes);
            else
                if (showTime)
                    tw.WriteLine(mes + "\t" + DateTime.Now.ToString());
                else
                    tw.WriteLine(mes);

            tw.Close();
        }
    }

    /// <summary>
    /// 输出日志信息
    /// </summary>
    /// <param name="mes">需要输出的信息</param>
    /// <param name="logType">信息标识</param>
    public static void Log(string mes,LogType logType)
    {
        using (TextWriter tw = File.AppendText(Application.dataPath + "/Log.txt"))
        {
            tw.WriteLine(TypeHead(logType) + mes + "\t" + DateTime.Now.ToString());
            tw.Close();
        }
    }

    /// <summary>
    /// 输出日志信息
    /// </summary>
    /// <param name="mes">需要输出的信息</param>
    /// <param name="showTime">是否显示时间</param>
    public static void Log(string mes,bool showTime)
    {
        using (TextWriter tw = File.AppendText(Application.dataPath + "/Log.txt"))
        {
            if (showTime)
                tw.WriteLine(TypeHead(LogType.Mes) + mes + "\t" + DateTime.Now.ToString());
            else
                tw.WriteLine(TypeHead(LogType.Mes) + mes);

            tw.Close();
        }
    }

    /// <summary>
    /// 输出日志信息
    /// </summary>
    /// <param name="mes">需要输出的信息</param>
    /// <param name="logType">信息标头</param>
    /// <param name="showTime">是否显示时间</param>
    public static void Log(string mes, LogType logType, bool showTime)
    {
        using (TextWriter tw = File.AppendText(Application.dataPath + "/Log.txt"))
        {
            if (showTime)
                tw.WriteLine(TypeHead(logType) + mes + "\t" + DateTime.Now.ToString());
            else
                tw.WriteLine(TypeHead(logType) + mes);

            tw.Close();
        }
    }

    #region TypeHead
    private static string TypeHead(LogType logType)
    {
        switch (logType)
        {
            case LogType.Error:
                return "[ERROR]:";
            case LogType.Warning:
                return "[WARNING]:";
            case LogType.Mes:
                return "[MES]:";
            case LogType.Tip:
                return "[TIP]:";
            case LogType.Other:
                return "[OTHER]:";
            default:
                return "[TYPERROR]:";
        }
    }
    #endregion
    
}

