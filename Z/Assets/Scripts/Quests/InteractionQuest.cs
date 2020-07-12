using UnityEngine;
using System.Collections;
using System;

public abstract class InteractionQuest : Quest
{

    public Interractable interractable;

    public override void StartQuest()
    {
        InteractionObjective objective = InstantiateObjective();
        StartupObjectives.Add(objective);
        InactiveObjectives.Add(objective);
        base.StartQuest();
    }

    private InteractionObjective InstantiateObjective()
    {
        GameObject go = new GameObject(QuestName + "_Objective");
        go.transform.SetParent(this.transform);

        InteractionObjective objective = go.AddComponent<InteractionObjective>();
        objective.interractable = interractable;

        return objective;
    }

}
