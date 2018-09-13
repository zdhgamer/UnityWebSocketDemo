using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAppMediator : Mediator
{
    /// <summary>
    /// 构造函数 获取UI界面元素，然后添加事件
    /// </summary>
    public StartAppMediator() {

    }

    /// <summary>
    /// 添加界面感兴趣的事件
    /// </summary>
    /// <returns></returns>
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(PurMVCEvents.AppStarted);
        return list;
    }

    /// <summary>
    /// 处理需要处理的事件
    /// </summary>
    /// <param name="notification"></param>
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name) {
            case PurMVCEvents.AppStarted:
                Debug.Log("App已启动");
                break;
        }
    }

}
