using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebug : MonoBehaviour {

    private static GameDebug instance;

    public static GameDebug Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// log日志
    /// </summary>
    /// <param name="message"></param>
    public static void Log(object message) {
#if SHOWLOG
        Debug.Log(message);
#endif
    }

    /// <summary>
    /// 错误日志
    /// </summary>
    /// <param name="message"></param>
    public static void LogError(object message) {
#if SHOWLOG
        Debug.LogError(message);
#endif
    }

    /// <summary>
    /// 警告日志
    /// </summary>
    /// <param name="messag"></param>
    public static void LogWarning(object messag) {
#if SHOWLOG
        Debug.LogWarning(messag);
#endif
    }

    /// <summary>
    /// 异常日志
    /// </summary>
    /// <param name="e"></param>
    public static void LogException(Exception e) {
#if SHOWLOG
        Debug.LogException(e);
#endif
    }

}
