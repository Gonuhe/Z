using UnityEngine;
using System.Collections;

public class EatSomethingQuest : InteractionQuest
{

    public override void StartQuest()
    {
        FindObjectOfType<ModuleManager>().HudManager.NotificationManager.ShowNotification("Here's you new home. Isn't it comfy ? After such a long trip, you should be hungry, you should eat something. Maybe your uncle left you a welcome present, you should check the fridge.");
        base.StartQuest();
    }
}
