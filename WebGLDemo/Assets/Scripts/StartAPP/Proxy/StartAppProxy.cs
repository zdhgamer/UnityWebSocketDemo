using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAppProxy : Proxy
{
    public new static string NAME = "StartAppProxy";

    private StartAppEntity startAppEntity;

    public StartAppProxy() : base(NAME) {
        startAppEntity = new StartAppEntity();
    }

    public StartAppEntity StartAppEntity
    {
        get
        {
            return startAppEntity;
        }

        set
        {
            startAppEntity = value;
        }
    }

    public void StartedApp() {
        startAppEntity.AppStarted = true;
        SendNotification(PurMVCEvents.AppStarted);
    }

}
