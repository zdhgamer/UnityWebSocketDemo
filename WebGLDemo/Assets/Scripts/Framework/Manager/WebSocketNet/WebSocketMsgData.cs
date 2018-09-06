using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;
using System;
using System.IO.Compression;

public class WebSocketMsgData
{

    public int msgId;

    public byte[] msMsgData;

    public WebSocketMsgData(int msgId, byte[] msgData)
    {
        this.msgId = msgId;
        this.msMsgData = new byte[msgData.Length];
        Array.Copy(msgData, msMsgData, msgData.Length);
    }

    public T DeDeserialize<T>()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            ms.Write(msMsgData, 0, msMsgData.Length);
            ms.Position = 0;
            return Serializer.Deserialize<T>(ms);
        }
    }
}