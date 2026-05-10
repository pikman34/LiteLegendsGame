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
    public static UIManager Instance;

    void Start()
    {
        interactionIcon.SetActive(false);
        Instance = UIManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactableInRange?.Interact();
        }

        if (player.GetComponent<CombatScript>().health <= 0)
        {
            Debug.Log("ayo you dead homie");
            player.GetComponent<CombatScript>().Death();
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
            player.GetComponent<CombatScript>().TakeDamage(10f);
            AudioSource.PlayClipAtPoint(oofSound, transform.position);
            UIManager.Instance.UpdateHealthBar(
                (int)player.GetComponent<CombatScript>().health,
                (int)player.GetComponent<CombatScript>().maxHealth
            );
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