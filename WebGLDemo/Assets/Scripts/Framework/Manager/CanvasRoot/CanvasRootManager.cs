using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRootManager : MonoBehaviour {

    private static CanvasRootManager instance;

    private Camera uiCamera;

    public static CanvasRootManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Camera UiCamera
    {
        get
        {
            if (uiCamera == null) {
                uiCamera = gameObject.transform.Find("UICamera").GetComponent<Camera>();
            }
            return uiCamera;
        }
    }

    private void Awake()
    {
        instance = this;

    }
}
