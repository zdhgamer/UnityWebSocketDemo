using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAppEntity {

    private bool appStarted = false;

    public bool AppStarted
    {
        get
        {
            return appStarted;
        }

        set
        {
            appStarted = value;
        }
    }
}
