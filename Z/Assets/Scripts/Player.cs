using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
public class Player : MonoBehaviour
{

    public GameObject currentInteraction;

    private ThirdPersonUserControl playerController;
    private ThirdPersonCharacter playerMover;

    private void Awake()
    {
        playerController = GetComponent<ThirdPersonUserControl>();
        playerMover = GetComponent<ThirdPersonCharacter>();
    }

    public void AllowToMove(bool b)
    {
        playerController.enabled = b;
        if (!b)
            StartCoroutine(StopPlayer());
    }

    //This is a simple DIRTY hack to force Unity's Fucking ThirdPersonUserControl. REMOVE IT and remove this hack
    private IEnumerator StopPlayer()
    {
        for (int i = 0; i < 25; i++) //Because of velocity we need to this over a few frames.
        {
            playerMover.Move(new Vector3(0, 0, 0), false, false);//on arr�te le joueur
            yield return null;
        }
    }
}
