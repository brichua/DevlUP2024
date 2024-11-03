using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int pinkWood;
    public static int blueWood;
    public static int brownWood;
    public static int purpleWood;
    public static int yellowWood;

    public static int redGem;
    public static int greenGem;
    public static int yellowGem;

    public static int charm;
    public static int flower;
    public static int herb;

    public static int stone;
    public static int mossyStone;

    public bool[] interactions_JaVale = new bool[4];
    public bool[] interactions_Baericks = new bool[4];
    public bool[] interactions_Gorgui = new bool[4];
    public bool[] interactions_Ocetire = new bool[4];

    public bool trigger_choice = false;

    public bool offering1;
    public bool offering2;
    public bool offering3;

    // REMEMBER TO CHANGE THIS BACK TO FALSE
    public static bool allInteractions = false;

    //Run a perpetual check to see if the player has triggered an ending
    private void Update()
    {
        if (allInteractions) {
            trigger_choice = true;
        }
    }



    //Check to see if the player has interacted with everyone.
    public void CheckInteraction(string character, int mood)
    {
        bool has_finished = true;
        mood -= 1;
        //Check JaVale
        if (character == "JaVale Andoris")
        {
            if (!interactions_JaVale[mood])
            {
                interactions_JaVale[mood] = true;
            }
        }
        //Check Baericks
        if (character == "Baericks Junifer")
        {
            if (!interactions_JaVale[mood])
            {
                interactions_Baericks[mood] = true;
            }
        }
        //Check Gorgui
        if (character == "Gorgui Bayers")
        {
            if (!interactions_JaVale[mood])
            {
                interactions_Gorgui[mood] = true;
            }
        }
        //Check Ocetire
        if (character == "Ocetire Sakoru")
        {
            if (!interactions_JaVale[mood])
            {
                interactions_Ocetire[mood] = true;
            }
        }
        //Check All
        for (int i = 0; i < interactions_JaVale.Length; i++)
        {
            if (interactions_JaVale[i] == false)
            {
                has_finished = false;
                break;
            }
        }
        for (int i = 0; i < interactions_Baericks.Length; i++)
        {
            if (interactions_Baericks[i] == false)
            {
                has_finished = false;
                break;
            }
        }
        for (int i = 0; i < interactions_Gorgui.Length; i++)
        {
            if (interactions_Gorgui[i] == false)
            {
                has_finished = false;
                break;
            }
        }
        for (int i = 0; i < interactions_Ocetire.Length; i++)
        {
            if (interactions_Ocetire[i] == false)
            {
                has_finished = false;
                break;
            }
        }
        allInteractions = has_finished;
    }
}
