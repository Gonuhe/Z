using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayerInterraction : Interractable
{
    AudioSource source;
    public AudioClip clip;

    public override void Interact(Player p)
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clip);
        StartCoroutine(WaitEndCoroutine(clip.length));
    }

    private IEnumerator WaitEndCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        InteractionFinished();
    }
}
