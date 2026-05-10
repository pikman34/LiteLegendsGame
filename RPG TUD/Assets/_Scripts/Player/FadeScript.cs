using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 5f;

    private void Start()
    {
        Color c = fadeImage.color;
        c.a = 0;
        fadeImage.color = c;
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeRoutine(0, 1));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadeRoutine(1, 0));
    }

    IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float timer = 0f;

        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        color.a = endAlpha;
        fadeImage.color = color;
    }
}