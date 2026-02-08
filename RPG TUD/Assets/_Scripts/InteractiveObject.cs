using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractiveObject : MonoBehaviour
{
    [Header("Interaction")]
    public string interactPrompt = "Press E to Interact";
    public bool oneTimeUse = true;
    public GameObject triggerObject;

    [Header("Animation")]
    public bool isAnimated;
    public Animator animator;
    public string interactTriggerName = "Interact";    

    [Header("Material ToHighlight")]
    public bool isHighlightable;
    public ObjectHighlighter materialToHighlight;

    protected bool isInteracted = false;



    public virtual void Interact()
    {
        if (oneTimeUse && isInteracted)
            return;

        isInteracted = true;

        if(isAnimated)
        {
            if (animator != null)
            {
                animator.SetTrigger(interactTriggerName);
                triggerObject.SetActive(false);
            }
        }                

    }


    public void HighlightMaterial()
    {
       materialToHighlight.EnableHighlight();
    }


    public void UnHighlightMaterial()
    {
        materialToHighlight.DisableHighlight();
    }


    public bool IsInteractedWith()
    {
        return isInteracted;
    }


}

