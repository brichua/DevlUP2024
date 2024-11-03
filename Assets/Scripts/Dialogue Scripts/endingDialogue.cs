using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static Unity.Burst.Intrinsics.X86;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;
using UnityEngine.Diagnostics;

public class endingDialogue : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public GameObject titleScreen;
    public GameObject startButton;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI messageText2;


    public string[] gameOver =
    {
       "Try as she must, the fire’s insatiable hunger is too overwhelming for Evelyn to keep up.",
       "Was the fire always this hungry? Did it always need this much resources?",
       "That much didn’t matter to the town, their cries echo throughout the village. The air was thick with the scent of ash and despair.",
       "The fire cared not for women or children, cared not for the elderly or infancy. The “protection” they relied on has been shattered, nothing exists to “protect” them now.",
       "The cries stop.",
       "...............",
       "What was the first offering again?",
       "Evelyn can’t seem to remember, but they do. Somehow, they do. And there is nothing you can do about it.",
        "The only thing that remains are tatters of red and orange cloth scattered on the earth.",
        "Another one will be found. Their “protection” will burn bright again. Burn, burn, and burn again…until they too will burn with it.",
        "The third offering has been submitted…"
    };

    public string[] badEnding =
    {
        "Evelyn clutches her fists with conviction and courage. She can’t let this fire go on for longer, she knows what it’s doing to the people.",
        "The longer the fire burns, the longer the people burn with it. Evelyn can’t let her people burn, can’t let this fire burn all the bridges they fought so hard to build up.",
        "Those weren’t dreams that Evelyn saw. She knows that the fire was responsible for all of it, that they were responsible for all of it.",
        "The stone Gods have long since abandoned this land. They were never angry at us, never cast a cold shoulder on us. They fought for us, fought for our protection, while beings like them took advantage of us, deceived us in exchange for 'protection.'",
        "Evelyn finds the closest sledgehammer and rope. She fastens her mother’s amulet to the tip of the hammer, intent on finishing the war once and for all.",
        "I know what you are. And I know how to beat you.",
        "Evelyn crashes her hammer into the center of the fire, the fire cries pleas of agony. They fall on deaf ears.",
        "Evelyn smashes and smashes, smashes all the flames away. It’s what her mother would have done. The “protection” has been lifted, the war has ended.",
        "Her people have been saved…",
        "......",
        "You’ve messed up dearly, foolish Petawaken.",
        "I don’t think I have.",
        "I know exactly what you are, Wenku Wepe.",
        "I won’t let you feed on my people anymore.",
        ".....",
        "The process was almost complete. I was this close.",
        "But I have enough to bring back the flames. I can wait 7 more centuries.",
        "Since you are in my way Petawaken, I’ll make sure you burn with the flames too.",
        "Evelyn watches in horror as the destroyed Hearth bursts aflame, far more intense than any of the documentation described it.",
        "But this fire wasn’t an ordinary file, this was different. This was an inferno. An unforgiving inferno. This fire required no upkeep, no stones or herbs or rituals would be needed to keep this fire ablaze.",
        "This fire offers no “protection,” this fire only seeks revenge, seeks ashes and destruction.",
        "The third offering was not submitted…",
        "The Ember Reckoning is in full effect, and only Evelyn stands a chance to put the flames out………",
        "....once and for all."
    };

    public string[] goodEnding =
    {
        "That’s right, you can’t the fire go out, the people depend on it, they depend on you.",
        "You can’t forsake your duties like this.",
        "No, no I can’t. Not after what I saw, they can’t live without it.",
        "No they can’t. It’s all they have left, it’s all they have to protect themselves. They’re powerless against the Gods.",
        "You of all people should know, Petawaken.",
        "Despite her hesitance, Evelyn understands that her ancestors were right all along. I mean, of course, you’re not going to be swayed to abandon a century-long tradition by some bad dreams, right?",
        "The people would suffer more if she abandons her duties. She can’t let her family name and loved ones down."
    };

    public Color[] gameOverColors = {
        Color.grey, Color.grey, Color.grey, Color.grey,
        Color.grey, Color.grey, Color.grey, Color.grey,
        Color.grey, Color.grey, Color.grey
    };

    public Color[] goodColors = {
        Color.white, Color.white, Color.white, Color.white,
        Color.white, Color.grey, Color.grey
    };

    public Color[] badColors =
    {
        Color.grey, Color.grey, Color.grey, Color.grey,
        Color.grey, Color.white, Color.grey, Color.grey,
        Color.grey, Color.grey, Color.red, Color.white,
        Color.white, Color.white, Color.red, Color.red,
        Color.red, Color.red, Color.red, Color.grey,
        Color.grey, Color.grey, Color.grey, Color.grey,
    };

    public float fadeDuration = 1f;



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

    IEnumerator gameOverMessages()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        startButton.SetActive(false);
        titleScreen.SetActive(false);
        foreach (string message in gameOver)
        {
            yield return ShowMessage(message);
        }
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
        yield return ShowMessage2("Press [N] to Open Inventory");
        Debug.Log("working");
    }

    IEnumerator goodEndingMessages()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        startButton.SetActive(false);
        titleScreen.SetActive(false);
        foreach (string message in goodEnding)
        {
            yield return ShowMessage(message);
        }
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
        yield return ShowMessage2("Press [N] to Open Inventory");
        Debug.Log("working");
    }

    IEnumerator badEndingMessages()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        startButton.SetActive(false);
        titleScreen.SetActive(false);
        foreach (string message in badEnding)
        {
            yield return ShowMessage(message);
        }
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
        yield return ShowMessage2("Press [N] to Open Inventory");
        Debug.Log("working");
    }

    IEnumerator ShowMessage(string message)
    {
        messageText.text = message;


        yield return StartCoroutine(FadeTextAlpha(messageText, 0, 1, fadeDuration));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeTextAlpha(messageText, 1, 0, fadeDuration));

        yield return new WaitForSeconds(0.5f);
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
