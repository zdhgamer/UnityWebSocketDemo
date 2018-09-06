using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketMsgCenter
{

    private Dictionary<int, List<Action<WebSocketMsgData>>> netActionCallBack = new Dictionary<int, List<Action<WebSocketMsgData>>>();

    /// <summary>
    /// 注册消息回调
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name=""></param>
    public void RegisterCallBack(int msgId, Action<WebSocketMsgData> callBack)
    {
        if (netActionCallBack.ContainsKey(msgId))
        {
            if (netActionCallBack[msgId] == null)
            {
                netActionCallBack[msgId] = new List<Action<WebSocketMsgData>>();
            }
            if (!netActionCallBack[msgId].Contains(callBack))
            {
                netActionCallBack[msgId].Add(callBack);
            }

        }
        else
        {
            List<Action<WebSocketMsgData>> temp = new List<Action<WebSocketMsgData>>();
            temp.Add(callBack);
            netActionCallBack.Add(msgId, temp);
        }

    }

    /// <summary>
    /// 移除所有消息回调
    /// </summary>
    /// <param name="msgId"></param>
    public void RemoveAllCallBack(int msgId)
    {
        if (netActionCallBack.ContainsKey(msgId))
        {
            netActionCallBack.Remove(msgId);
        }
    }

    /// <summary>
    /// 移除一个消息回调
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="callBack"></param>
    public void RemoveCallBack(int msgId, Action<WebSocketMsgData> callBack)
    {
        if (netActionCallBack.ContainsKey(msgId))
        {
            if (netActionCallBack[msgId] != null && netActionCallBack[msgId].Count > 0)
            {
                if (netActionCallBack[msgId].Contains(callBack))
                {
                    netActionCallBack[msgId].Remove(callBack);
                }
                if (netActionCallBack[msgId].Count <= 0 || netActionCallBack[msgId] == null)
                {
                    netActionCallBack.Remove(msgId);
                }
            }
            else
            {
                netActionCallBack.Remove(msgId);
            }
        }
    }

    /// <summary>
    /// 分发消息
    /// </summary>
    /// <param name="msgId"></param>
    public void DispatchMsg(int msgId, WebSocketMsgData data)
    {
        if (netActionCallBack.ContainsKey(msgId))
        {
            for (int i = 0; i < netActionCallBack[msgId].Count; i++)
            {
                netActionCallBack[msgId][i]?.Invoke(data);
            }
        }
        RemoveAllCallBack(msgId);
    }

}
