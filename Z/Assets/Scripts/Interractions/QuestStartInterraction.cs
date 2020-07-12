using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStartInterraction : Interractable
{
    public Quest quest;

    public override void Interact(Player p)
    {
        FindObjectOfType<QuestManager>().StartQuest(quest);
    }
}
