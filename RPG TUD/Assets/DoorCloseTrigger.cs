using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string closeTriggerName = "Close";

    private bool hasClosed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasClosed) return;

        if (other.CompareTag("Player"))
        {
            hasClosed = true;

            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger(closeTriggerName);
            }
            gameObject.SetActive(false);
        }
    }
}