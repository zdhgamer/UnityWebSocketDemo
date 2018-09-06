using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;
using System.Text;
using ProtoBuf;

public class WebSocketNetManager : MonoBehaviour
{

    private ClientWebSocket webSocket = new ClientWebSocket();

    private WebSocketMsgCenter webSocketMsgCenter = new WebSocketMsgCenter();

    private Action onCloseConnect;

    private static WebSocketNetManager m_instance;

    public static WebSocketNetManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    public Action OnCloseConnect
    {
        get
        {
            return onCloseConnect;
        }

        set
        {
            if (onCloseConnect == null)
            {
                onCloseConnect = value;
            }
            else
            {
                onCloseConnect += value;
            }
        }
    }

    private void Awake()
    {
        m_instance = this;
    }

    /// <summary>
    /// 注册消息回调
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="callBack"></param>
    public void RegisterCallBack(int msgId, Action<WebSocketMsgData> callBack)
    {
        webSocketMsgCenter.RegisterCallBack(msgId, callBack);
    }

    /// <summary>
    /// 移除所有消息回调
    /// </summary>
    /// <param name="msgId"></param>
    public void RemoveAllCallBack(int msgId)
    {
        webSocketMsgCenter.RemoveAllCallBack(msgId);
    }

    /// <summary>
    /// 移除一个消息回调
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="callBack"></param>
    public void RemoveCallBack(int msgId, Action<WebSocketMsgData> callBack)
    {
        webSocketMsgCenter.RemoveCallBack(msgId, callBack);
    }

    /// <summary>
    /// 异步连接服务器
    /// </summary>
    /// <param name="callBack"></param>
    public async void ConnetServerAsync(Action<bool> callBack)
    {
        await webSocket.ConnectAsync(new System.Uri(AppConst.WebSocketServerUrl), new CancellationToken());
        if (webSocket.State == WebSocketState.Open)
        {
            StartReceiving();
            callBack?.Invoke(true);
        }
        else
        {
            callBack?.Invoke(false);
        }
    }

    /// <summary>
    /// 异步发送消息
    /// </summary>
    /// <param name="msdId"></param>
    /// <param name="callBack"></param>
    public async void SendMessageAsync<T>(int msgId, T msg, Action<WebSocketMsgData> callBack = null)
    {
        if (callBack != null)
        {
            RegisterCallBack(msgId, callBack);
        }

        if (webSocket.State == WebSocketState.Open)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] mid = System.Text.Encoding.Default.GetBytes(msgId + "");
                Serializer.Serialize(stream, msg);
                byte[] bytes = stream.ToArray();
                byte[] send = new byte[mid.Length + bytes.Length];
                Array.Copy(mid, send, mid.Length);
                Array.Copy(bytes, 0, send, mid.Length, bytes.Length);
                await webSocket.SendAsync(new ArraySegment<byte>(send), WebSocketMessageType.Binary, true, new CancellationToken());
            }
        }
        else
        {
            OnCloseConnect?.Invoke();
        }
    }

    /// <summary>
    /// 异步接受消息
    /// </summary>
    /// <param name="client"></param>
    private async void StartReceiving()
    {
        while (true)
        {
            if (webSocket == null)
            {
                break;
            }
            var array = new byte[4096];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(array), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Binary)
            {
                int msgId = int.Parse(Encoding.Default.GetString(array, 0, 4));
                GameDebug.Log("收到消息id:" + msgId);
                byte[] tem = new byte[array.Length];
                Array.Copy(array, tem, array.Length);
                byte[] receive = new byte[result.Count - 4];
                Array.Copy(array, 4, receive, 0, result.Count - 4);
                WebSocketMsgData data = new WebSocketMsgData(msgId, receive);
                webSocketMsgCenter.DispatchMsg(data.msgId, data);
            }
        }
    }


    private void OnDestroy()
    {
        webSocket.Dispose();
        webSocket = null;
    }

}
