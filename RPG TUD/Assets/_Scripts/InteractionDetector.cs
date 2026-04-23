using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InteractionDetector : MonoBehaviour
{
    private IInteractables interactableInRange = null; //Closest Interactable
    public GameObject interactionIcon;

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactableInRange?.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IInteractables interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IInteractables interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}