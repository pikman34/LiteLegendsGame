using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public string dialoguePrompt = "Press E to Talk";

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
