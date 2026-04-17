using UnityEngine;

public class QuestChecker : MonoBehaviour
{
    public DialogueTrigger dt;
    public Dialogue questCompleteDialogue;
    public Quest questToBeChecked;

    public void TriggerQuestCheck()
    {
        if (questToBeChecked.state == QuestState.Completed)
        {
            dt.dialogue = questCompleteDialogue;
        }
    }
}
