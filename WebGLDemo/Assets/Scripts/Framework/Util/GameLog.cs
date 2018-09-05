using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameLog : MonoBehaviour {

    private static GameLog instance;

    /// <summary>
    /// 日志连表
    /// </summary>
    private List<string> logList = new List<string>();

    /// <summary>
    /// 文件写入地址
    /// </summary>
    private string url = Application.persistentDataPath+"/"+ AppConst.AppName;

    private string filePath = "";

    public static GameLog Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        filePath = url + "/GameLog.txt";

        if (File.Exists(filePath)) {
            File.Delete(filePath);
        }
        File.Create(filePath).Dispose();

        Application.logMessageReceived += OnLogMessageReceived;
    }

    void Update()
    {
        if (logList.Count > 0)
        {
            string value = logList[0];
            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                writer.WriteLine(value + "\n" + "");
                writer.WriteLine("////****************************************万恶的分割线****************************************///");
                writer.Close();
            }
            logList.RemoveAt(0);
        }
    }

    //当收到系统日志消息时
    private void OnLogMessageReceived(string condition, string stackTrace, LogType type) {
        switch (type) {
            case LogType.Error:
            case LogType.Log:
            case LogType.Warning:
            case LogType.Exception:
                if (string.IsNullOrEmpty(stackTrace))
                {
                    logList.Add(System.DateTime.Now + "\n" + condition + "\n" + Environment.StackTrace + "\n" + type.ToString());
                }
                else
                {
                    logList.Add(System.DateTime.Now + "\n" + condition + "\n" + stackTrace + "\n" + type.ToString());
                }

                break;
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= OnLogMessageReceived;
    }

}
