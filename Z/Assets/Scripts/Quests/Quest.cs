using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class Quest : MonoBehaviour
{
    public string QuestName;

    private bool Finished = false;//TODO script editeur pour que ce champ soit en lecture seule depuis l'inspecteur

    public List<Quest> NextQuests = new List<Quest>();


    public List<Objective> StartupObjectives = new List<Objective>();

    public List<Objective> InactiveObjectives = new List<Objective>();

    public List<Objective> ActiveObjectives = new List<Objective>();

    public List<Objective> FinishedObjectives = new List<Objective>();


    public virtual void StartQuest()
    {
        if (StartupObjectives.Count == 0)
        {
            Debug.LogWarning("Started a quest with no objective, it will finish instantly");
            FinishQuest();
        }
        foreach (Objective objective in StartupObjectives)
            StartObjective(objective);
    }

    public void StartObjective(Objective objective)
    {
        bool b = InactiveObjectives.Remove(objective);
        if (b)
        {
            ActiveObjectives.Add(objective);
            objective.gameObject.SetActive(true);
            objective.StartObjective(this);
        }
        else
            throw new System.InvalidOperationException("Trying to start an objective that hasn't been registered in this Quest. Abort. Please register the objective in the inspector for this quest.");
    }

    public void FinishObjective(Objective objective)
    {
        bool b = ActiveObjectives.Remove(objective);
        if (!b)
            throw new System.InvalidOperationException("Finished an objective that is not active or is not an objective of this quest :" + objective.name);

        objective.gameObject.SetActive(false);
        FinishedObjectives.Add(objective);

        foreach (Objective o in objective.NextObjectives)
            StartObjective(o);

        if (ActiveObjectives.Count == 0)
            FinishQuest();
    }

    protected virtual void FinishQuest()
    {
        Finished = true;
        QuestManager manager = FindObjectOfType<QuestManager>();
        manager.FinishQuest(this);
        foreach (Quest q in NextQuests)
            manager.StartQuest(q);
    }
}
