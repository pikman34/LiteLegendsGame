using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    private Quest currentQuest;
    public UIManager uiManager;

    private Dictionary<string, Quest> quests = new Dictionary<string, Quest>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterQuest(Quest quest)
    {
        if (!quests.ContainsKey(quest.questID))
            quests.Add(quest.questID, quest);
    }

    public QuestState GetQuestState(string questID)
    {
        return quests.ContainsKey(questID)
            ? quests[questID].state
            : QuestState.NotStarted;
    }

    public void StartQuest(string questID)
    {
        if (!quests.ContainsKey(questID)) return;

        quests[questID].state = QuestState.InProgress;
        currentQuest = quests[questID];
        Debug.Log("Quest Started: " + questID);

        StartCoroutine(DisplayQuest());
 
    }

    public void CompleteQuest(string questID)
    {
        if (!quests.ContainsKey(questID)) return;

        quests[questID].state = QuestState.Completed;
        Debug.Log("Quest Completed: " + questID);
        uiManager.ClearPromptText();
    }





    IEnumerator DisplayQuest()
    {
        uiManager.DisplayQuest(true, currentQuest.questTitle, currentQuest.description);
        yield return new WaitForSeconds(5);
        uiManager.DisplayQuest(false, "", "");
    }


    public string GetCurrentQuestID()
    {
        return currentQuest.questID;
    }


    public bool IsQuestActive()
    {
        if (quests.Count > 0)
            return true;
        else
            return false;
    }

}
