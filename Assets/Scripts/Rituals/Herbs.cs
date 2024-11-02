using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Herbs : Interactable
{
    public CanvasGroup fadePanel;
    public TextMeshProUGUI messageText;
    public string[] messages;
    public float fadeDuration = 1f;
    public float messageDisplayTime = 2f;

    private bool playerInRange = false;
    public static bool isPickedUp = true;
    public static bool appeared = false;

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(PickupSequence());
    }

    IEnumerator PickupSequence()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));

        foreach (string message in messages)
        {
            yield return StartCoroutine(ShowMessage(message));
        }

        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));

        gameObject.SetActive(false);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        cg.alpha = startAlpha;
        cg.blocksRaycasts = true;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        cg.alpha = endAlpha;
        cg.blocksRaycasts = endAlpha == 1;
    }

    IEnumerator ShowMessage(string message)
    {
        messageText.text = message;
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        yield return new WaitForSeconds(messageDisplayTime);
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
    }

}
