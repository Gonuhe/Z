using UnityEngine;
using System.Collections;
using System;

public class TakeShowerQuest : InteractionQuest
{
    public override void StartQuest()
    {
        FindObjectOfType<ModuleManager>().HudManager.NotificationManager.ShowNotification("You are dirty, you should take a shower !");
        base.StartQuest();
    }
}
