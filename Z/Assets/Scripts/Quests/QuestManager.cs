using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    //TODO faire des propriétés et exposer correctement (seul StartupQuests et InactiveQuests peut ne pas être readonly)
    public List<Quest> StartupQuests;

    public List<Quest> InactiveQuests;

    public List<Quest> ActiveQuests;

    private List<Quest> FinishedQuests = new List<Quest>();

    void Start ()
    {
        foreach(Quest quest in StartupQuests)
            StartQuest(quest);
    }

    public void StartQuest(Quest quest)
    {
        bool b = InactiveQuests.Remove(quest);
        if (b)
        {
            ActiveQuests.Add(quest);
            quest.gameObject.SetActive(true);
            quest.StartQuest();
        }
        else
            throw new System.InvalidOperationException("Trying to start a quest that hasn't been registered in the QuestManager. Abort. Please register the quest in the QuestManager by adding it to InactiveQuests");
    }

    public void FinishQuest(Quest quest)
    {
        bool b = ActiveQuests.Remove(quest);
        if (!b)
            throw new System.InvalidOperationException("Finished a Quest that is not active :" + quest.QuestName);

        Debug.Log("Quest Finished :" + quest.QuestName);

        quest.gameObject.SetActive(false);
        FinishedQuests.Add(quest);
    }

}
