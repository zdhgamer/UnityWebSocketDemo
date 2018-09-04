using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MyWebSocket : MonoBehaviour {

    public Text text;

    private int num;

    private ClientWebSocket webSocket = new ClientWebSocket();

    // Use this for initialization
    void Start () {
        Task<string> task = connectWebServer();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private async Task<string> connectWebServer() {
        await webSocket.ConnectAsync(new System.Uri("ws://127.0.0.1:8282"), new CancellationToken());
        if (webSocket.State == WebSocketState.Closed)
        {
            Debug.Log("fail");
            return "fail";
        }
        else {

            StartReceiving(webSocket);

            byte[] bytes = Encoding.UTF8.GetBytes("hello zdh");

            await webSocket.SendAsync(new System.ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, new CancellationToken());


            Debug.Log("success");


            return "success";
        }
 
    }

    private async void StartReceiving(ClientWebSocket client)
    {
        while (true)
        {
            var array = new byte[4096];
            var result = await client.ReceiveAsync(new ArraySegment<byte>(array), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string msg = Encoding.UTF8.GetString(array, 0, result.Count);
                Debug.Log(msg);
                text.text = num++.ToString();
            }
        }
    }


    private void OnDestroy()
    {
        webSocket.Dispose();
        webSocket = null;
    }

}
