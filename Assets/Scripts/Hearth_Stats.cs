using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
        }
        //Check if it's critical
        else if (currentHealth <= 25)
        {
            if (mood != 1)
            {
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
}
