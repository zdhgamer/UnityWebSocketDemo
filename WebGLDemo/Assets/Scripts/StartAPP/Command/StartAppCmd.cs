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

    }
}
