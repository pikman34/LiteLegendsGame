using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject spellWheel;
    private CanvasGroup canvasGroup;

    private bool isPaused = false;
    public float fadeSpeed = 5f;

    void Start()
    {
        canvasGroup = spellWheel.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!isPaused) Pause();
        }
        else
        {
            if (isPaused) Resume();
        }
    }

    public void Resume()
    {
        StartCoroutine(FadeOut());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        spellWheel.SetActive(true);
        StartCoroutine(FadeIn());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.5f;
        isPaused = true;
    }

    IEnumerator FadeIn()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    IEnumerator FadeOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }

        canvasGroup.alpha = 0;
        spellWheel.SetActive(false);
    }
}