using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interractable //TODO : s'il y a d'autres interractions sur le même objet, elles seront déclanchée en même temps, et donc pendant le fondu au noir.
{
    public Door target;
    private const float FADE_DURATION = 0.5f;

    private Player _p;

    protected override void Awake()
    {
        if (target == null)
            Debug.LogWarning("The door with name (" + gameObject.name + ") does not have a target. It will not work.");
        else if (target.target != this)
            Debug.LogWarning("The target of this door does not have this door as target. There might be a problem."); //TODO implement a feature to remove this warning if this was intended
        base.Awake();
    }

    public override void Interact(Player p)
    {
        _p = p;
        if (target == null)
            throw new InvalidOperationException("Door has no target.");
        else
        {
            UIFadeToColorController hudFateToDark = FindObjectOfType<UIFadeToColorController>();
            if (hudFateToDark != null)
            {
                hudFateToDark.targetColor = Color.black;
                hudFateToDark.targetColor.a = 1;
                hudFateToDark.duration = FADE_DURATION;
                hudFateToDark.OnFadeDone.AddListener(Teleport);
                hudFateToDark.Fade();
            }
            else
                Teleport();


        }
    }

    private void Teleport()
    {
        StartCoroutine(TeleportCoroutine());
    }

    private IEnumerator TeleportCoroutine()
    {
        _p.transform.position = target.transform.position;
        _p.transform.rotation = target.transform.rotation;
        yield return new WaitForSeconds(1);

        UIFadeToColorController hudFateToDark = FindObjectOfType<UIFadeToColorController>();
        if (hudFateToDark != null)
        {
            hudFateToDark.OnFadeDone.RemoveListener(Teleport);
            hudFateToDark.targetColor.a = 0;
            hudFateToDark.OnFadeDone.AddListener(InteractionFinished);
            hudFateToDark.Fade();
        }
        else
            InteractionFinished();
    }
}
