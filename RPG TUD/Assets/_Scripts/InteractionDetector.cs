using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InteractionDetector : MonoBehaviour
{
    private IInteractables interactableInRange = null; //Closest Interactable
    public GameObject interactionIcon;
    public GameObject player;
    public AudioClip coinSound;
    public AudioClip oofSound;

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
        if (other.CompareTag("InventoryItem"))
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        if (other.CompareTag("Arrow"))
        {
            player.GetComponent<CombatScript>().health -= 10f;
            Debug.Log("Player Health: " + player.GetComponent<CombatScript>().health);
            AudioSource.PlayClipAtPoint(oofSound, transform.position);
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