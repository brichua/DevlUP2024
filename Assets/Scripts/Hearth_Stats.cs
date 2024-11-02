using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth_Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Boolean hearthLife = true;

    public void removeHealth(int damage) 
    { 
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            hearthLife = false;
        }
    }
}
