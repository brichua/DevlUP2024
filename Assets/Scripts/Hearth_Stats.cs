using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject weakFire;
    public GameObject moderateFire;
    public GameObject strongFire;
    public GameObject strongestFire;
    public Image fadeImage;
    public GameObject strongestBar;
    public GameObject strongBar;
    public GameObject moderateBar;
    public GameObject weakBar;
    public CanvasGroup fadePanel;
    public GameObject endScreen;
    public TextMeshProUGUI messageText;

    public string[] messages1 = {
        
    };

    public string[] messages2 = {

    };

    public float fadeDuration = 1f;
    private int count = 0;


    void Start()
    {
        // Preps the stage for having all game objects be true
        strongestFire.SetActive(true); strongFire.SetActive(false); moderateFire.SetActive(false); weakFire.SetActive(false);
        mood = 4;
        StartCoroutine(CountDownHealth());
    }

    private void Update()
    {
        //Check if it's Dead
        if (currentHealth == 0)
        {
            mood = 0;
            if (Player.allInteractions)
            {
                StartCoroutine(BadEnding());
            }
        }
        //Check if it's critical
        else if (currentHealth <= 25)
        {
            if (mood != 1)
            {
                strongestBar.SetActive(false);
                strongBar.SetActive(false);
                moderateBar.SetActive(false);
                weakBar.SetActive(true);
                if (mood == 4)
                {
                    switchObjects(strongestFire, weakFire);
                }
                else if (mood == 3)
                {
                    switchObjects(strongFire, weakFire);
                }
                else if (mood == 2)
                {
                    switchObjects(moderateFire, weakFire);
                }
                else { Debug.Log("Something wrong with dying fire Update() in Hearth_Stats"); }
                mood = 1;
            }

        }
        //Check if it's Waning
        else if (currentHealth <= 50)
        {
            strongestBar.SetActive(false);
            strongBar.SetActive(false);
            moderateBar.SetActive(true);
            weakBar.SetActive(false);
            if (mood != 2)
            {
                if (mood == 4)
                {
                    switchObjects(strongestFire, moderateFire);
                }
                else if (mood == 3)
                {
                    switchObjects(strongFire, moderateFire);
                }
                else if (mood == 1)
                {
                    switchObjects(weakFire, moderateFire);
                }
                else { Debug.Log("Something wrong with waning fire Update() in Hearth_Stats"); }
                mood = 2;
            }
        }
        //Check if it's Healthy
        else if (currentHealth <= 75)
        {
            strongestBar.SetActive(false);
            strongBar.SetActive(true);
            moderateBar.SetActive(false);
            weakBar.SetActive(false);
            if (mood != 3)
            {
                if (mood == 4)
                {
                    switchObjects(strongestFire, strongFire);
                }
                else if (mood == 2)
                {
                    switchObjects(moderateFire, strongFire);
                }
                else if (mood == 1)
                {
                    switchObjects(weakFire, strongFire);
                }
                else { Debug.Log("Something wrong with healthy fire Update() in Hearth_Stats"); }
                mood = 3;
            }
        }
        //Check if it's (bullshit?) Blazing
        else if (currentHealth <= maxHealth)
        {
            strongestBar.SetActive(true);
            strongBar.SetActive(false);
            moderateBar.SetActive(false);
            weakBar.SetActive(false);
            if (mood != 4) 
            {
                if (mood == 3)
                {
                    switchObjects(strongFire, strongestFire);

                }
                else if (mood == 2)
                {
                    switchObjects(moderateFire, strongestFire);
                }
                else if (mood == 1) 
                {
                    switchObjects(weakFire, strongestFire);
                }
                else { Debug.Log("Something wrong with blazing fire Update() in Hearth_Stats"); }
                mood = 4;
            }
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
        //tp player here
        yield return StartCoroutine(FadeFromBlack());
        //animation stuff
        if (Player.allInteractions)
        {
            StartCoroutine(Ending1());
        }
        else
        {
            StartCoroutine(Ending2());
        }
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

    IEnumerator Ending1()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        endScreen.SetActive(true);
        foreach (string message in messages1)
        {
            yield return ShowMessage(message);
        }
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));
    }

    IEnumerator Ending2()
    {
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));
        endScreen.SetActive(true);
        foreach (string message in messages2)
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
