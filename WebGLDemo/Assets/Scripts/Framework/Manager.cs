using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private CanvasRootManager canvasRootManager;

    private HttpNetManager httpNetManager;

    private WebSocketNetManager webSocketNetManager;

    public void InitManagers()
    {
        this.gameObject.AddComponent<NotDestoryGo>();

        if (canvasRootManager == null)
        {
            GameObject UICanvas = GameObject.Find("UICanvas");
            if (UICanvas != null)
            {
                canvasRootManager = UICanvas.AddComponent<CanvasRootManager>();
                UICanvas.AddComponent<NotDestoryGo>();
            }
        }
        if (httpNetManager == null)
        {
            httpNetManager = this.gameObject.AddComponent<HttpNetManager>();
        }
        if (webSocketNetManager == null)
        {
            webSocketNetManager = this.gameObject.AddComponent<WebSocketNetManager>();
        }

    }


}
