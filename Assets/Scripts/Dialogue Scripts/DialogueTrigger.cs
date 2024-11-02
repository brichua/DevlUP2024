using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System.IO.Pipes;


public class DialogueTrigger : MonoBehaviour
{
    //Attach this script to an empty gameobject with a 2D collider set to trigger
    DialogueManager manager;
    //GameManager gameManager = new GameManager();
    public TextAsset TextFileAsset; // your imported text file for your NPC
    private Queue<string> dialogue = new Queue<string>(); // stores the dialogue (Great Performance!)
    public float waitTime = 0.5f; // lag time for advancing dialogue so you can actually read it
    private float nextTime = 0f; // used with waitTime to create a timer system
    public bool singleUseDialogue = false;
    [HideInInspector]
    public bool hasBeenUsed = false;
    public bool inArea = false;
    public bool in_dialogue;
    public bool trigger;

    // public bool useCollision; // unused for now
    //Differentiate between the NPCs
    public GameObject NPC;
    string path = "Assets/Scripts/Dialogue Scripts/Dialogue Text.txt";
    public NPC_Manager NPC_Manager;

    //Interctible Sprite
    public GameObject interactible_sprite;

    private void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
        //Writer
        //Make sure file exists
        if (!File.Exists(path))
        {
            Debug.Log("The file does not exist.");
        }
        AssetDatabase.Refresh();
        
    }


    private void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.E) && !in_dialogue) {
            in_dialogue = true;
            trigger = true;
        }
        if (!hasBeenUsed && inArea && Input.GetKeyDown(KeyCode.E) && nextTime < Time.timeSinceLevelLoad)
        {
            //Debug.Log("Advance");
            nextTime = Time.timeSinceLevelLoad + waitTime;
            manager.AdvanceDialogue();
        }
        //Load Interactable Sprite
        if (inArea && !in_dialogue)
        {
            interactible_sprite.SetActive(true);
        }
        else
        {
            interactible_sprite.SetActive(false);
        }
    }

    /* Called when you want to start dialogue */
    void TriggerDialogue()
    {
        ReadTextFile(); // loads in the text file
        manager.StartDialogue(dialogue); // Accesses Dialogue Manager and Starts Dialogue
    }

    /* loads in your text file */
    private void ReadTextFile()
    {
        string txt = TextFileAsset.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split dialogue lines by newline

        SearchForTags(lines);

        dialogue.Enqueue("EndQueue");
    }

    /*Version 2: Introduces the ability to have multiple tags on a single line! Allows for more functions to be programmed
     * to unique text strings or general functions. 
     */
    private void SearchForTags(string[] lines)
    {
        foreach (string line in lines) // for every line of dialogue
        {
            if (!string.IsNullOrEmpty(line))// ignore empty lines of dialogue
            {
                if (line.StartsWith("[")) // e.g [NAME=Michael] Hello, my name is Michael
                {
                    string special = line.Substring(0, line.IndexOf(']') + 1); // special = [NAME=Michael]
                    string curr = line.Substring(line.IndexOf(']') + 1); // curr = Hello, ...
                    dialogue.Enqueue(special); // adds to the dialogue to be printed
                    string[] remainder = curr.Split(System.Environment.NewLine.ToCharArray());
                    SearchForTags(remainder);
                    //dialogue.Enqueue(curr);
                }

                else
                {
                    dialogue.Enqueue(line); // adds to the dialogue to be printed
                }
            }
        }

    }

    public void UpdateText(string charName, int moodState, bool short_text)
    {
        if (charName == "JaVale Andoris")
        {
            //Happy
            if (moodState == 4) {
                if (short_text) {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Such a warm, lovely day outside! Perfect for choppin’ down some trees. Gotta find my axes though…");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Oh, it’s you Evelyn. How have you been doing?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Good Mr. Andoris! How about you!");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]I’m just my jolly old self per usual! The shop has been doing good and the missus isn’t yelling at me over laundry!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Haha, that’s good to hear.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]How’s the good old fire job treatin’ ya? Hopefully it ain’t too much weight on your back.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Yeah, it’s going quite well so far. Didn’t expect to inherit it so quickly, but that’s how things are sometimes.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Ha, you can say that again! My dad had me cutting lumber before I knew how to say “Timber”! And you know I’m gonna be passing that tradition on to me boy.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Maybe you shouldn’t do that, he’s going to hate you.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Ahhhhahaha, I was only playin’ Evelyn, I got enough strong arms here to help me.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Welp, I won’t keep ya here, I know you got business to attend to. You can always swing by if you need good, strong lumber!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Thank you Mr. Andoris. Take care!");
                        writer.Close();
                    }
                }
            }
            
        }
        AssetDatabase.Refresh();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hasBeenUsed)
        {
            manager.currentTrigger = this;
            //if (in_dialogue) { TriggerDialogue(); }
            //Debug.Log("Collision");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = true;
            if (in_dialogue && trigger) {
                //Check to see if dialogue needs to be updated
                UpdateText(NPC.name, NPC_Manager.mood, NPC_Manager.short_interaction);
                TriggerDialogue();
                trigger = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.EndDialogue();
            in_dialogue = false;
        }
        if (!NPC_Manager.short_interaction)
        {
            NPC_Manager.UpdateShortInteraction();
        }
        inArea = false;
    }
}
