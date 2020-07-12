using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : MonoBehaviour
{

    private bool Finished = false; //TODO script editeur pour que ce champ soit en lecture seule depuis l'inspecteur

    public List<Objective> NextObjectives = new List<Objective>();

    private Quest _quest;

    public virtual void StartObjective(Quest parent)
    {
        _quest = parent;
    }

    /// <summary>
    /// Must be called in children
    /// </summary>
    protected virtual void FinishObjective()
    {
        Finished = true;
        _quest.FinishObjective(this);
    }
}
