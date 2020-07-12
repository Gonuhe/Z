using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjective : Objective
{

    public Interractable interractable;

    public override void StartObjective(Quest parent)
    {
        if (interractable == null)
            throw new InvalidOperationException("Interaction quest doesn't have an Interractable target. Please set one up.");
        interractable.OnAfterInteraction.AddListener(FinishObjective);
        base.StartObjective(parent);
    }

    protected override void FinishObjective()
    {
        interractable.OnAfterInteraction.RemoveListener(FinishObjective);
        base.FinishObjective();
    }
}
