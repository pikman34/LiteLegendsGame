using System;
using UnityEngine;

public class PlayerObjectTrigger : MonoBehaviour
{
    public InventoryManager inventory;
    public QuestManager questManager;
    private string currentQuestID;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InventoryItem")
        {
            InventoryItem inventoryItem = other.GetComponent<InventoryItem>();
            Item otherItem = inventoryItem.GetItem();
            inventory.AddItem(otherItem);
            other.gameObject.SetActive(false);
        }

        if (other.tag == "QuestItem")
        {
            QuestItem questData = other.GetComponent<QuestItem>();  

            if (questManager.IsQuestActive())
            {
                currentQuestID = questManager.GetCurrentQuestID();

                // easier to read
                string objectQuestID = questData.GetQuest().questID;

                if (objectQuestID.Contains(currentQuestID))
                {
                    questManager.CompleteQuest(currentQuestID);
                    other.gameObject.SetActive(false);
                }
            }    
            
        }

    }
}
