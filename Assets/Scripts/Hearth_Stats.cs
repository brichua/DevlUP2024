using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Hearth_Stats : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int currentHealth = 100;
    public static Boolean hearthLife = true;
    //Variable for Village Mood: 0 = Dead, 1 = Critical, 2 = Waning, 3 = Healthy, 4 = Blazing (Bullshit?)
    public static int mood;
    public GameObject player;
    public Animator fireImage;
    public Image barImage;
    public GameObject shadow;
    public AnimatorController weakFire;
    public AnimatorController moderateFire;
    public AnimatorController strongFire;
    public AnimatorController strongestFire;
    public Image fadeImage;
    public Sprite strongestBar;
    public Sprite strongBar;
    public Sprite moderateBar;
    public Sprite weakBar;
    public CanvasGroup fadePanel;
    public GameObject endScreen;
    public TextMeshProUGUI messageText;

    public string[] messages1 = {
        "Try as she must, the fire’s insatiable hunger is too overwhelming for Evelyn to keep up.",
        "Was the fire always this hungry? Did it always need this much resources?",
        "That much didn’t matter to the town, their cries echo throughout the village. The air was thick with the scent of ash and despair.",
        "The fire cared not for women or children, cared not for the elderly or infancy. The “protection” they relied on has been shattered, nothing exists to “protect” them now.",
        "The cries stop.",
        "……………………",
        "The footsteps grow near. They can’t lose their “protection,” no matter what, they have to reignite the flames.",
        "What was the first offering again?",
        "Evelyn can’t seem to remember, but they do. Somehow, they do. And there is nothing you can do about it.",
        "The only thing that remains are tatters of red and orange cloth scattered on the earth.",
        "Another one will be found. Their “protection” will burn bright again. Burn, burn, and burn again…until they too will burn with it.",
        "The third offering has been submitted…"
    };

    public float fadeDuration = 1f;
    private int count = 0;


    void Start()
    {
        // Preps the stage for having all game objects be true
        fireImage.runtimeAnimatorController = strongestFire;
        mood = 4;
    }

    private void Update()
    {
       // Debug.Log(openingDialogue.done);
        if (openingDialogue.done)
        {
            StartCoroutine(CountDownHealth());
            openingDialogue.done = false;
        }
        Debug.Log(currentHealth);
        //Check if it's Dead
        if (currentHealth <= 0)
        {
            mood = 0;
            StartCoroutine(BadEnding());
        }
        //Check if it's critical
        else if (currentHealth <= 25)
        {
            mood = 1;
            fireImage.runtimeAnimatorController = weakFire;
            barImage.sprite = weakBar;
        }
        //Check if it's Waning
        else if (currentHealth <= 50)
        {
            mood = 2;
            fireImage.runtimeAnimatorController = moderateFire;
            barImage.sprite = moderateBar;
        }
        //Check if it's Healthy
        else if (currentHealth <= 75)
        {
            mood = 3;
            fireImage.runtimeAnimatorController = strongFire;
            barImage.sprite = strongBar;
        }
        //Check if it's (bullshit?) Blazing
        else if (currentHealth <= maxHealth)
        {
            mood = 4;
            fireImage.runtimeAnimatorController = strongestFire;
            barImage.sprite = strongestBar;
        }
    }

    IEnumerator CountDownHealth() 
    {
        // hold off for 36 seconds
        yield return new WaitForSeconds(.5f);
        RemoveHealth(1);
        StartCoroutine(CountDownHealth());
    }

    public void RemoveHealth(int damage) 
    { 
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            hearthLife = false;
        }
        Debug.Log("Hearth health is " + currentHealth);
    }

    public static void AddHealth(int healing) 
    {
        currentHealth += healing;
        if (currentHealth > maxHealth) 
        { 
            currentHealth = maxHealth;
        }
    }

    public void switchObjects(GameObject one, GameObject two) 
    {
        one.SetActive(false);
        two.transform.position = one.transform.position;
        two.SetActive(true);
    }

    private IEnumerator BadEnding()
    {
        yield return StartCoroutine(FadeToBlack());
        player.transform.position = fireImage.transform.position;
        Debug.Log("RAaaaa");
        shadow.transform.position = fireImage.transform.position;
        yield return StartCoroutine(FadeFromBlack());
        //player.transform.position = this.transform.position;
        shadow.transform.position = fireImage.transform.position;
        StartCoroutine(Ending());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;

        while (elapsedTime < 0.5f)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / 0.5f);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f;

        while (elapsedTime < 0.5f)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / 0.5f);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
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

    IEnumerator Ending()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        endScreen.SetActive(true);
        foreach (string message in messages1)
        {
            yield return ShowMessage(message);
        }
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
    }

    IEnumerator ShowMessage(string message)
    {
        messageText.text = message;
        messageText.color = Color.white;

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
