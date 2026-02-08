using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    public TMP_Text text;
    public float moveSpeed = 2f;
    public float lifetime = 1f;

    private void Start()
    {
        text.enabled = false;
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        text.enabled = true;
        yield return new WaitForSeconds(lifetime);
        text.enabled = false;

    }
}
