using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : Interactable
{
    public CanvasGroup fadePanel;
    public TextMeshProUGUI messageText;
    public GameObject coin;

    public string[] messages = {
        "Heavens almighty, is it that hard to understand!? You have to hold your hands like this, you’re doing it too lazily.",
        "I-I’m sorry dad, I’m trying.",
        "Clearly you are not, it’s not that difficult! You keep forgetting everything, keep messing up the rituals, keep bringing the wrong ingredients.",
        "Do you even understand how important this is? How much people are going to depend on you to do this job the right way?",
        "DO YOU UNDERSTAND WHAT THEY’LL DO TO US IF WE DON’T FEED HIM.",
        "Dad, calm down, please.",
        "You notice the very young teenager start to break down in tears, and the person who you assume to be the person’s dad, starts to calm down.",
        "Stay here, I’m going to fetch something for you.",
        "The father leaves the fire area, and the kid continues to cry. Soon after though, the kid changes mood as if possessed.",
        "Filthy fire, this is all your fault! You’re just a dumb fire, why can’t you just go away.",
        "The kid begins recklessly bashing the books into the flames. The fire, however, consumed them like a beast devoid of food for centuries; hundreds of records lost.",
        "The kid then turns towards a box of valuable trinkets meant for the rituals. He begins flinging them at the fire with ultimate violence.",
        "Go away, GO AWAY.",
        "You see the father rush onto the scene, but it’s too late. The only thing that remains is a coin that the child dropped on the floor amongst the chaos.",
        "The second offering has been submitted…",
        "WHAT IN GOD'S NAME ARE YOU DOING? GET AWAY FROM THE FIRE YOU IDIOT.",
        "The child prepares to slam the now empty box which formerly contained the trinkets into the flames.",
        "However, the child loses his footing, and falls straight into the flames. The father helplessly watches on as the fire consumed the one thing he loved.",
        "The fire returns to its normal state, seemingly unaffected by the heaps of objects and violence thrown at it.",
        "The father stood as still as a charred tree after a wildfire, rooted in place, his eyes hollow and empty, as if the flames had burned away every last trace of feeling.",
        "The second offering has been accepted…"

    };

    public Color[] messageColors = {
        Color.white, Color.yellow, Color.white, Color.yellow,
        Color.yellow, Color.white, Color.yellow, Color.grey,
        Color.yellow, Color.grey, Color.grey, Color.yellow,
        Color.grey, Color.grey, Color.white, Color.grey, Color.grey,
        Color.grey, Color.grey, Color.grey
    };

    public float fadeDuration = 1f;

    private bool playerInRange = false;
    public static bool isPickedUp = true;
    public static bool appeared = false;
    private int count = 0;


    public override void Interact()
    {
        base.Interact();
        StartCoroutine(PickupSequence());
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

    IEnumerator PickupSequence()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        coin.SetActive(false);
        foreach (string message in messages)
        {
            yield return ShowMessage(message);
        }

        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
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
