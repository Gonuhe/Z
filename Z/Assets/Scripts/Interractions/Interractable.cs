using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Multiple components are supported. You can put multiple interractable on the same GameObject and all interraction will be triggered. However if, multiple interraction on multiple gameobjects cannot be triggered at the same time to avoid conflicts.
/// </summary>
[RequireComponent (typeof(Collider))]
public abstract class Interractable : MonoBehaviour
{

    GameObject InteractionHint;

    public UnityEvent OnBeforeInteraction = new UnityEvent();
    public UnityEvent OnAfterInteraction = new UnityEvent();

    protected virtual void Awake()
    {
        Transform interactionHintTransform = transform.Find("InteractionHint");
        if (interactionHintTransform == null)
            Debug.LogWarning("The interractable with name (" + gameObject.name + ") does not have an interaction hint.");
        else
            InteractionHint = interactionHintTransform.gameObject;

        if(InteractionHint != null)
            InteractionHint.SetActive(false);
    }

    public abstract void Interact(Player p);

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (InteractionHint != null)
                InteractionHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (InteractionHint != null)
                InteractionHint.SetActive(false);
        }
    }

    //Will be called after 'update' so, if the interaction calls InteractionFinished in update, there is no risk that the interaction will be triggered again during the same frame.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player p = FindObjectOfType<Player>(); //TODO passer par un gameManager ou changer de mécanisme
                if (p.currentInteraction == null || p.currentInteraction == gameObject)
                {
                    p.currentInteraction = gameObject;
                    StartCoroutine(TriggerInteraction(p));
                }
                else
                    Debug.Log("An interaction has been triggered on " + gameObject.name + " but an interraction is already taking action in another gameobject (" + p.currentInteraction.name + ") this one will not be performed."); //TODO établir un système de priorité
            }
        }
    }

    //Do some stuff like show an animation, etc. By default, we simply wait for the next frame for the input to be consumed by the interractable.
    public IEnumerator TriggerInteraction(Player p)
    {
        yield return null;
        if (OnBeforeInteraction != null)
            OnBeforeInteraction.Invoke();
        Interact(p);
    }

    public virtual void InteractionFinished()
    {
        FindObjectOfType<Player>().currentInteraction = null;
        if (OnAfterInteraction != null)
            OnAfterInteraction.Invoke();
    }
}
