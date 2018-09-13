using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAppCmd : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        GameObject goMgr = GameObject.Find(AppConst.GameManagerName);
        if (goMgr == null)
        {
            goMgr = new GameObject(AppConst.GameManagerName);
        }
        Manager manager = goMgr.GetComponent<Manager>();
        if (manager == null)
        {
            manager = goMgr.AddComponent<Manager>();
        }
        manager.InitManagers();

        StartAppProxy startAppFacade = (StartAppProxy)Facade.RetrieveProxy(StartAppProxy.NAME);
        startAppFacade.StartedApp();
    }
}
