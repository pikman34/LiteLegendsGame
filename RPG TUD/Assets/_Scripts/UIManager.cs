using TMPro;
using UnityEngine;


using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider xpBar;
    public GameObject damagePopup;
    public static UIManager Instance;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public GameObject questWindow;
    public DialogueManager dialogueManager;
    private bool promptActive = false;


    private void Start()
    {
        promptText.text = "";
    }

    private void OnEnable()
    {
        PlayerInteraction.OnInteractableTriggered += ShowPrompt;
        PlayerInteraction.OnDialogueTriggered += ShowDialogue;
        InventoryManager.OnInventoryChange += UpdatePromptDisplay;
    }
    private void OnDisable()
    {
        PlayerInteraction.OnInteractableTriggered -= ShowPrompt;
        PlayerInteraction.OnDialogueTriggered -= ShowDialogue;
        InventoryManager.OnInventoryChange -= UpdatePromptDisplay;
    }



    public void UpdateHealthBar(int current, int max)
    {
        healthBar.value = (float)current / max;
    }

    public void UpdateXPBar(int current, int max)
    {
        xpBar.value = (float)current / max;
    }

    public void ShowDamagePopup(int amount)
    {
        damagePopup.GetComponent<DamagePopup>().SetText(amount);
    }

    public void ShowPrompt(bool show, InteractiveObject interactiveObject)
    {
        if(show)
        {
            promptText.text = interactiveObject.interactPrompt;
            promptActive = true;
            if(interactiveObject.isHighlightable)
                interactiveObject.HighlightMaterial();
        }
        else
        {
            // hide
            promptText.text = "";
            promptActive = false;
            if(interactiveObject.isHighlightable)
                interactiveObject.UnHighlightMaterial();
            interactiveObject = null; 
        }      
    }


    public void ShowDialogue(bool show, DialogueTrigger trigger)
    {
        if (show)
        {
            dialogueManager.StartDialogue(trigger.dialogue);
        }
        else
        {
            dialogueManager.EndDialogue();
        }
    }


    public void DisplayQuest(bool display, string title, string description)
    {
        if(display)
        {
            questTitle.text = title;
            questDescription.text = description;
            questWindow.SetActive(true);
        }
        else
        {
            questTitle.text = "";
            questDescription.text = "";
            questWindow.SetActive(false);
        }
       
    }


    public void UpdatePromptDisplay()
    {
        if(promptActive)
            promptText.text = "";
    }


    public void ClearPromptText()
    {
        promptText.text = "";
    }



}
