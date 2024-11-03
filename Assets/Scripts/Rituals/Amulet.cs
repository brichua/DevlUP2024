using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Amulet : Interactable
{
    public CanvasGroup fadePanel;
    public GameObject amulet;
    public TextMeshProUGUI messageText;

    public string[] messages = {
        "I told you, there was something wrong with the fire. Why is it that everytime I forget to feed it, everyone is losing their minds?",
        "I-I don’t know honey. Our ancestors would not have let it burn on this long if something was wrong though.",
        "I don’t know how to describe it. But the fire, it’s controlling us. It’s not the safe flame that we think it is, I just know it!",
        "But the villagers will kill us if we destroy it, we just can’t.",
        "But this fire, it’s killing them. It doesn’t protect us from the cold anymore, it only uses the cold to control us.",
        "Wait, I think I figured it out.",
        "You see your father rummage through a bookcase of historical records, until he finds a book on the Stone Gods.",
        "Each of the records here indicate some sort of resolution to our God’s Stories, like how Wíčaglata took flight to the skies to defend against the Celestial Furnace. But I couldn’t help but notice that one story looked odd.",
        "The story for Wenku Wepe, it’s borderline incomplete. There isn’t an incredible feat like the other Gods, just “set out for greater horizons.” It’s nonsense, we haven’t been able to discover anything about him.",
        "I had my suspicions too. It must be them, I know it. But why aren’t there any records on them?",
        "There must be a reason for that, I just know it. A lot of records are missing, this is why we know so little.",
        "But what should we do? Let the fire burn out? What if we’re wrong?",
        "We’re not wrong, I know it. The fire is already weakening, we just need to let it die naturally and then-",
        "A dissonant voice appears to come from the fire. It sounds miserable almost.",
        "Feeeeed meeeeee. Don’t let me dieeeeeee.",
        "Pleeeeaseeeee.",
        "Don’t listen to it, we have to go. I have a bad feeling about this.",
        "FEEEEEEEED MEEEEEE.",
        "Just as quick as you witness your parents turn around, they are swept into the fire by an overwhelming force. You see no signs of a struggle, no screams of agony. Nothing.",
        "After what feels like an eternity of the fire blazing uncontrollably, it calms down to what you recognize to be in its healthy state.",
        "Nobody was around to witness the “accident,” nobody saw what that monster did to your parents, to your only family.",
        "Nobody knows where they went, but everyone guesses what happened. All you can see now is a girl with dark brown hair and a red styled outfit approach the fire and pick up the amulet of her mother, the amulet that she promised to give to her on the day she would inherit the job from her.",
        "The third offering has been submitted…",
        "…………………", "Or has it?"

    };

    public Color[] messageColors = {
        Color.white, Color.yellow, Color.white, Color.yellow,
        Color.white, Color.yellow, Color.grey, Color.white,
        Color.white, Color.yellow, Color.white, Color.white,
        Color.yellow, Color.grey, Color.red, Color.red, Color.white,
        Color.red, Color.grey, Color.grey, Color.grey, Color.grey, Color.grey,
        Color.grey, Color.grey
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
        amulet.SetActive(false);
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
