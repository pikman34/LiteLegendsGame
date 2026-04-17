using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<QuestProgress> activateQuests = new();
    private QuestUI questUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        questUI = FindAnyObjectByType<QuestUI>();
        InventoryManager.Instance.OnInventoryChanged += CheckInventoryForQuests;
    }

    public void AcceptQuest(Quests quest)
    {
        if (IsQuestActive(quest.questID)) return;

        activateQuests.Add(new QuestProgress(quest));

        questUI.UpdateQuestUI();
    }

    public bool IsQuestActive(string questID) => activateQuests.Exists(q => q.QuestID == questID);

    public void CheckInventoryForQuests()
    {
        Dictionary<int, int> itemCounts = InventoryManager.Instance.GetItemCounts();

        foreach(QuestProgress quest in activateQuests)
        {
            foreach(QuestObjective questObjective in quest.objectives)
            {
                if (questObjective.type != ObjectiveType.CollectItem) continue;
                if(!int.TryParse(questObjective.objectiveID, out int itemID)) continue;

                int newAmount = itemCounts.TryGetValue(itemID, out int count) ? Mathf.Min(count, questObjective.requiredAmount) : 0;

                if (questObjective.currentAmount != newAmount)
                {
                    questObjective.currentAmount = newAmount;
                }
            }
        }

        questUI.UpdateQuestUI();
    }
}
