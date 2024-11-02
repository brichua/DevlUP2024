using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearth_Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Boolean hearthLife = true;
    //Variable for Village Mood: 0 = Dead, 1 = Critical, 2 = Waning, 3 = Healthy, 4 = Blazing (Bullshit?)
    public int mood;

    void Start()
    {
        StartCoroutine(CountDownHealth());
    }

    IEnumerator CountDownHealth() 
    {
        // hold off for 36 seconds
        yield return new WaitForSeconds(.5f);
        RemoveHealth(1);
        CheckMood();
        StartCoroutine(CountDownHealth());
    }

    public void RemoveHealth(int damage) 
    { 
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            hearthLife = false;
        }
        Debug.Log("Hooked on a feeling " + currentHealth);
    }

    public void AddHealth(int healing) 
    {
        currentHealth += healing;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
    }

    public void CheckMood()
    {
        //Check if it's Dead
        if (currentHealth == 0) {mood = 0;}
        //Check if it's critical
        else if (currentHealth <= maxHealth / 4) {mood = 1;}
        //Check if it's Waning
        else if (currentHealth <= maxHealth / 2) { mood = 2; }
        //Check if it's Healthy
        else if (currentHealth <= maxHealth / (4/3)) { mood = 3; }
        //Check if it's Blazing
        else if (currentHealth <= maxHealth) { mood = 4; }
    }
}
