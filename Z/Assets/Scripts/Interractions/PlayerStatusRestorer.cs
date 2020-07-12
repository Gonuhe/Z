using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class PlayerStatusRestorer : Interractable
{
    public float RestoreAmount = 50;

    public PlayerStatusType type;

    private PlayerStatus playerStatus;

    public override void Interact(Player p)
    {
        playerStatus.GetPlayerStatusElement(type).Restore(RestoreAmount);
        InteractionFinished();
    }

    protected override void Awake()
    {
        base.Awake();
        playerStatus = FindObjectOfType<PlayerStatus>();
    }
}
