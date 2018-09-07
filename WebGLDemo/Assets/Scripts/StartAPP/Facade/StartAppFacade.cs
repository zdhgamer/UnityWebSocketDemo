using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAppFacade : Facade {

    public new static IFacade Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance == null) m_instance = new StartAppFacade();
                }
            }

            return m_instance;
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public StartAppFacade() : base(){

    }

    /// <summary>
    /// 初始化model
    /// </summary>
    protected override void InitializeModel()
    {
        base.InitializeModel();
        RegisterProxy(new StartAppProxy());
    }

    /// <summary>
    /// 初始化View
    /// </summary>
    protected override void InitializeView()
    {
        base.InitializeView();
        RegisterMediator(new StartAppMediator());
    }

    /// <summary>
    /// 初始化Controller
    /// </summary>
    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(PurMVCEvents.StratApp, typeof(StartAppCmd));
    }

    /// <summary>
    /// 发送启动app事件
    /// </summary>
    public void StartApp()
    {
        SendNotification(PurMVCEvents.StratApp);
    }

}
