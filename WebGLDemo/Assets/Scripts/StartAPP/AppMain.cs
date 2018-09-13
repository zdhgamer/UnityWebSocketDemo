using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMain : MonoBehaviour
{

    private void Awake()
    {
        ((StartAppFacade)StartAppFacade.Instance).StartApp();
        this.gameObject.AddComponent<NotDestoryGo>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
