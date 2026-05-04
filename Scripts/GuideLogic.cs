using UnityEngine;
using System.Collections;
using TMPro;

public class GuideLogic : MonoBehaviour
{
    [SerializeField] private string guideText;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float delayBeforeStart = 1f;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;

        if (!string.IsNullOrEmpty(guideText))
        {
            TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
                textComponent.text = guideText;
            StartCoroutine(FadeInOutSequence());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeInOutSequence()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        // Fade in
        yield return Fade(0f, 1f, fadeInDuration);

        // Display
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return Fade(1f, 0f, fadeOutDuration);

        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}