using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuestStartTrigger : MonoBehaviour
{
    public Quest quest;

    void Start ()
    {
        Collider c = GetComponent<Collider>();
        if (c == null)
            Debug.LogError("QuestStartTrigger does not have a collider.");
        if (!c.isTrigger)
            Debug.LogError("QuestStartTrigger's collider is not a trigger.");
    }


    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<QuestManager>().StartQuest(quest);
        Destroy(gameObject); //TODO gérer l'instantiation et la sauvegarde de ce trigger
    }
}
