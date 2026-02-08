using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;

    public static Action<bool, InteractiveObject> OnInteractableTriggered;
    public static Action<bool, DialogueTrigger> OnDialogueTriggered;

    private InteractiveObject currentInteractable;
    private ObjectHighlighter currentHighlight;
    private DialogueTrigger currentDialogue;

  
    void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive())
            return;


        if (Input.GetKeyDown(interactKey))
        {
            if (currentDialogue != null)
            {
                currentDialogue.TriggerDialogue();
                return;
            }

            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        currentDialogue = other.GetComponentInParent<DialogueTrigger>();
        currentInteractable = other.GetComponentInParent<InteractiveObject>();

        if (currentDialogue != null)
        {
            if (OnDialogueTriggered != null)
                OnDialogueTriggered(true, currentDialogue);
        }
        else if (currentInteractable != null)
        {
            if (!currentInteractable.IsInteractedWith())
            {
                if (OnInteractableTriggered != null)
                    OnInteractableTriggered(true, currentInteractable);
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<DialogueTrigger>() != null)
        {
            if (OnDialogueTriggered != null)
                OnDialogueTriggered(false, currentDialogue);
        }

        if (other.GetComponentInParent<InteractiveObject>() != null)
        {
            if (OnInteractableTriggered != null)
                OnInteractableTriggered(false, currentInteractable);
        }

    }
}

    





