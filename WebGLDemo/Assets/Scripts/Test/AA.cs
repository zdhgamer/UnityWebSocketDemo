using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;
using System;

[ProtoContract]
public class WebSocketMsgDataTest
{
    [ProtoMember(1)]
    public int idd;
}

public class AA : MonoBehaviour {

    public void OnClick() {
        WebSocketMsgDataTest test = new WebSocketMsgDataTest();
        test.idd = 1;
        WebSocketNetManager.Instance.SendMessageAsync(1010, test, (msg) =>
        {
            WebSocketMsgDataTest receive = msg.DeDeserialize<WebSocketMsgDataTest>();
            Debug.Log(receive.idd);
        });
    }

	// Use this for initialization
	void Start () {
        WebSocketNetManager.Instance.ConnetServerAsync((result) =>
        {
            if (result) {
                GameDebug.Log("服务器连接成功");
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
