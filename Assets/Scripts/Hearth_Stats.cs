using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth_Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Boolean hearthLife = true;

    void Start()
    {
        StartCoroutine(CountDownHealth());
    }

    IEnumerator CountDownHealth() 
    {
        // hold off for 36 seconds
        yield return new WaitForSeconds(36);
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
    }

    public void AddHealth(int healing) 
    {
        currentHealth += healing;
        if(currentHealth > maxHealth) { currentHealth = maxHealth; }
    }
}
