using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    private bool isDialogueActive;
    private bool isDialogueCompleted = false;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.T))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(!isDialogueCompleted)
        {
            dialoguePanel.SetActive(true);
            lines.Clear();
            isDialogueActive = true;

            foreach (var line in dialogue.lines)
                lines.Enqueue(line);

            DisplayNextLine();
        }
       
    }

    void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines.Dequeue();

        speakerText.text = line.speaker;
        dialogueText.text = line.text;

        

        if (line.questToStart != null)
        {
            QuestManager.Instance.RegisterQuest(line.questToStart);
            QuestManager.Instance.StartQuest(line.questToStart.questID);
        }

        if (line.questToComplete != null)
        {
            QuestManager.Instance.CompleteQuest(line.questToComplete.questID);
            Debug.Log("questToComplete");
        }
        Debug.Log("DisplayNextLine");
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        isDialogueCompleted = true;
        StartCoroutine("ResetDialog");
    }


    IEnumerator ResetDialog()
    {
        yield return new WaitForSeconds(2);
        isDialogueCompleted = false;

    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}

