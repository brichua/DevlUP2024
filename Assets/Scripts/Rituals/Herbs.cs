using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Herbs : Interactable
{
    public CanvasGroup fadePanel;
    public GameObject herbs;
    public TextMeshProUGUI messageText;

    public string[] messages = {
        "Hey, wait, what are you doing? You can’t go outside, it’s too dangerous.",
        "But look at what awaits us outside. So much space! Why would we waste away in caves like these?",
        "They will get angry at us if we go outside, did you not hear of what happened to Asmanda’ha? ",
        "Yeah, but he just didn’t do it right. I’m telling you, we just need to lay a foundation and get the blessing of one of the Stone Gods.",
        "How do you plan on doing that? They hate us for what we did centuries ago, you would be lucky to get the blessing of even a foul devil.",
        "SJIBFVGDSBFHKDSJABHKAAFREWJ.",
        "What was that!?",
        "Mmmmnnnsshhhhh, a blessing you say?",
        "What are you, what do you want with us?",
        " I’ve already stated my purpose, fool. Do you need a blessing or not?",
        "Uhhhh, yes. Yes we do. I want to venture outsi-",
        "Hush hush, I’ve already heard the whole reason.",
        "No, you can’t do this, can’t you see this thing is trouble?",
        "This is our only chance, can’t you see? Why are you so insistent on staying in these caves?",
        "This thing will only bring us trouble and ruin. You can’t risk what we’ve built up over the years.",
        "BUT WE HAVE TO, CAN’T YOU SEE THE WORLD OF OPPORTUNITY OUTSIDE.",
        "NO, GET OVER HERE, IT’S TIME TO GO.",
        "I WON’T LET YOU ROB ME OF THIS CHANCE.",
        "The sound of a fight breaks loose, and eventually you can hear the sound of a crack on the stone floor.",
        "The first offering has been submitted…",
        "So now what?",
        "I’ll be taking this fellow. We need him for the fire.",
        "The Fire?",
        "Why of course. How else are you supposed to survive the conditions outside?",
        "Well, ok.",
        "You see the mysterious person walk outside, seemingly with a barrier of protection around him.",
        "He walks and walks, until he’s told to stop by a dissonant voice.",
        "Here shall do.",
        "The body of the other mysterious person is dropped from its state of levitation, and a fire blazes from thin air from where the body was dropped.",
        "The fire instantly drowns out the unbearable cold, and herbs sprout seemingly out of nowhere near the fire.",
        "The first offering has been accepted…"
    };

    public Color[] messageColors = {
        Color.white, Color.yellow, Color.white, Color.yellow,
        Color.white, Color.red, Color.white, Color.red,
        Color.white, Color.red, Color.yellow, Color.red,
        Color.yellow, Color.yellow, Color.white, Color.yellow,
        Color.white, Color.yellow, Color.grey, Color. grey,
        Color.white, Color.red, Color.white, Color.red, Color.white,
        Color.grey, Color.grey, Color.red, Color.grey, Color.grey, Color.grey
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
        herbs.SetActive(false);
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
