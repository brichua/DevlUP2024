using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class openingDialogue : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public GameObject titleScreen;
    public GameObject startButton;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI messageText2;
    public GameObject book;
    public GameObject player;
    public GameObject bar;

    public string[] messages = {
        "Ugh, this is so boring. Why can’t I just hang out with my friends today?",
        "Have some respect, Evelyn. I know this seems boring, but it is very important for the village. We must not let the fire go out at any cost, remember?",
        "Yeah, yeah mom, I know. But why are we even collecting all this weird stuff? Can’t we just throw sticks into the fire?",
        "It’s not as simple my dear. You see, sticks were good enough when we started the fire, but it no longer works with just sticks. You will inherit all the rituals the village discovered over time. Only through these rituals are we able to keep the fire lit.",
        "But it’s just a regular fire!",
        "Far from it, this fire is what keeps us warm and lively. If it starts dying down, our moods also die down with it. Are you even paying attention in school?",
        "Of course mom. So…what happens if the fire goes out?", "…",
        "We don’t know honey, and hopefully you shouldn’t have to worry, the fire has not dropped to critical danger level for 96 years. You just have to do the rituals I teach you, and the job will be easier than making your favorite Pumpkin Pie.",
        "You know mom, all my friends try to spook me with stories about the fire. Saying that some ghost or something haunts it.",
        "…", "Hahaha, your friends are funny. You don’t have to worry, no such thing exists.",
        "We’re almost finished with the preparations! You can get your dad now, you can go play with your ghost friends or whatever after.",
        "Yay!", "A ghost haunting the fire? Wonder where they heard that from."
    };

    public Color[] messageColors = {
        Color.yellow, Color.white, Color.yellow, Color.white,
        Color.yellow, Color.white, Color.white, Color.yellow,
        Color.white, Color.yellow, Color.yellow, Color.white,
        Color.white, Color.white, Color.yellow, Color.white
    };

    public float fadeDuration = 1f;

    private bool playerInRange = false;
    public static bool isPickedUp = true;
    public static bool appeared = false;
    public static bool done = false;
    private int count = 0;

    public void StartGame()
    {
        StartCoroutine(OpeniningSequence());
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

    IEnumerator OpeniningSequence()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        startButton.SetActive(false);
        titleScreen.SetActive(false);
        foreach (string message in messages)
        {
            yield return ShowMessage(message);
        }
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
        book.SetActive(true);
        bar.SetActive(true);
        player.SetActive(true);
        yield return ShowMessage2("Press [N] to Open Inventory");
        done = true;
        Debug.Log("working");
    }

    IEnumerator ShowMessage(string message)
    {
        messageText.text = message;

        Debug.Log(messageColors[count]);
        messageText.color = messageColors[count];

        yield return StartCoroutine(FadeTextAlpha(messageText, 0, 1, fadeDuration));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeTextAlpha(messageText, 1, 0, fadeDuration));

        yield return new WaitForSeconds(0.5f);
        count++;
    }

    IEnumerator ShowMessage2(string message)
    {
        messageText2.text = message;

        yield return StartCoroutine(FadeTextAlpha(messageText2, 0, 1, fadeDuration));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeTextAlpha(messageText2, 1, 0, fadeDuration));
    }

    IEnumerator FadeTextAlpha(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        Color color = text.color;
        color.a = startAlpha;
        text.color = color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            text.color = color;
            yield return null;
        }

        color.a = endAlpha;
        text.color = color;
    }

}
