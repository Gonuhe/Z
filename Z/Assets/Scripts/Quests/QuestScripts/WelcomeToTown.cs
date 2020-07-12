using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeToTown : InteractionQuest
{
    public override void StartQuest()
    {
        FindObjectOfType<ModuleManager>().HudManager.NotificationManager.ShowNotification("Welcome to town ! The city is wonderful and full of surprises. You'll get plenty of time to explore, now you should go home.");//TODO ne pas mettre ça comme première quête. Cela ouvre tout tout de suite, trop vite, alors qu'on a encore rien accroché chez le joueur.
        base.StartQuest();
    }
}
