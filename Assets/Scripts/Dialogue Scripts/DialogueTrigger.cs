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
        Debug.Log("Started");
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
        //Javale's Dialogue
        if (charName == "JaVale Andoris")
        {
            //Happy
            if (moodState == 4)
            {
                if (short_text)
                {
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
            //Moderate
            else if (moodState == 3)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Getting a bit chilly. I might as well get lumber before it gets hotter.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hi Mr. Andoris. How’s your day going?");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Oh, you know, business as usual. Lost one of my axes today, which is a bummer. Poor fella is probably buried under some dirt right now.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I’m sorry to hear.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Ah, it’s no big fuss. If I lose one axe, I can replace it with two! That being said, I can’t lose too many, otherwise the missus will never let me hear the end of how much money I spend on em’.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Fair enough, I’m sure my mom would have done the same.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]I still have my old reliable though! She’s always sharp and ready. I mean, I try to keep her sharp. Sometimes it works. But hey, even a kinda-sharp blade keeps the logs coming, and the business sorta roaring. ");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Ahem, what I meant to say was don’t let yourself down kid. Don’t let the thrill of having such an important job rob you of the joys of youth. Don’t let the flame in your heart die!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Thank you Mr. Andoris, I’m managing quite well.");
                        writer.WriteLine("[NAME=JaVale's Wife][SPEAKERSPRITE=JaVale's Wife]JaVale! The laundry!");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Oh flapjack flippers, I thought I just did it. Coming over honey!");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Duty calls I suppose. Take care Evelyn.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hehe, you too Mr. Andoris.");
                        writer.Close();
                    }
                }
            }
            //Sad
            else if (moodState == 2)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]I don’t feel like working today, not in the mood. The fire’s getting weak, don’tcha think?");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris].....");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Mr. Andoris?");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Oh, hello Evelyn. ");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]So…how’s the lumber business?");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]It’s alright. Nothing new.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, I see.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Been getting a bit cold as of late. No good for some lumber gathering.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]...");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]I hope you don’t mind me asking, but it’ll get warmer soon, right?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I’m working on it, don’t you worry.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Good, good.");
                        writer.WriteLine("[NAME=JaVale's Wife][SPEAKERSPRITE=JaVale's Wife]JAVALE. PLEASE TAKE OUT THE TRASH.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Give me a second honey, I’m talking to someone!");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Sorry about that, she’s been very cranky as of late.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]It’s alright, no need to worry.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]I’m gonna close up shop a bit early today kid. Take care.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Thanks, you too.");
                        writer.Close();
                    }
                }
            }
            //Depressed
            else if (moodState == 1)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]JaVale: .....Shop is closed today. Come back later, I guess.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]...");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hello Mr. Andoris.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]......");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hello?");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Hi.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]...");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Not the day for lumber today, sorry kiddo. Missus hasn’t been talking to me either, always giving me the cold shoulder.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]...");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Fire hasn’t been that weak in a while, like a tree trunk in a desert.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]I don’t think your mother would be too proud of that.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Sorry about that.");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]......");
                        writer.WriteLine("[NAME=JaVale Andoris][SPEAKERSPRITE=JaVale Andoris]Scurry along kiddo, I got work to finish up right now.");
                        writer.Close();
                    }
                }
            }

        }
        //Baericks's Dialogue
        else if (charName == "Baericks Junifer")
        {
            //Happy
            if (moodState == 4)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Trinkets here! Or I could tell you about the legend of Weeping Wendy!");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Trinkets! Get your special trinkets! Oh, it’s you Evelyn. Here for trinkets?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, I’m just taking a look around Ms. Junifer. Find anything special as of late?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Unfortunately, nothing much. I haven’t gone exploring in a while myself, and I’m still waiting for a crew I dispatched to return.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, where are they at?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Oh, just sent them down south. There’s a system of caves down in that direction, they’re exploring so far.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Yikes, that sounds a bit dangerous though.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Relax, although it’s dangerous for people like us, I hired professionals. They shouldn’t go too deep either.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]I’m just interested if there’s anything of historical significance to document, some cave paintings, or, I don’t know, uhhhh artifacts of some kind.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I see, so what are you doing now?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Why, uhhhhh............I am manning the shop, yes, manning the shop! This is very difficult to do, you know. So many people try to lowball me for all my trinkets, these are art!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Welp, I’m going to take care of other things. Have fun running your drawings shop.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Why of course, have a good da- WAIT, DRAWING SHOP?!?!?");
                        writer.Close();
                    }
                }
            }
            //Moderate
            else if (moodState == 3)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]I haven’t gotten an update from the expedition in a while. Hmmmmmm, maybe I could send someone to check on them……………………I hope they’re ok.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Good evening, how may I assist you today?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]It’s me Ms. Junifer.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Oh, sorry Evelyn, I wasn’t looking. How are you?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Doing good.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]By the way, I heard you mumbling about some Weeping Wendy a while ago. Do you know much about that?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Ah, so you have heard of it too.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Well, I don’t know how much you know, but Weeping Wendy is the name of a rumored entity");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]It’s said to inhabit the flame that you keep lit, and letting it go out would make it very upset.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Yeah, I have heard of all of that.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]I imagine so. Unfortunately, I have not made much progress on being able to discover more about this entity, if it even exists.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Yeah, maybe a lot of information was lost to history. If I’m honest, I know little of why we keep the flame lit. ");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Well, best not to question the logic of our forefathers. They knew most about this I’m afraid.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]You best believe I’m on it!");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]That’s the spirit. Who knows what kind of destruction the end of the flame could bring.");
                        writer.Close();
                    }
                }
            }
            //Sad
            else if (moodState == 2)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]It can’t be, can it? They’re supposed to be experienced, I’m sure it was safe. No, nonono. It’s just some regular caves, don’t be silly. They’ll be fine.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hello Ms. Junifer, is everything well?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Ughhhhh, I have not been resting well. My energy is very low. I’ll probably hold off on my research for a bit.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, I see. Been hearing that from a lot of people.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]The flame looks a bit dim. I’m sure you got it under control.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I’m trying my best.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Anyways, I don’t know if I already told you, but I sent a crew to explore some caves, and I’m afraid they have not returned, or really sent any status updates of the kind.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, that’s very concerning. Is there anything I can do to help?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Don’t worry, I’m sure they all got it under control. I even sent another messenger just in case, although I would not be surprised if they take a while.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Just, please keep that flame lit, I feel like I’m close to cracking the code, I just need a little more time.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]The code?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Hahaha.....just the code to my sweets safe after all. G-gotta keep the sweet tooth i-in check, you know?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I see.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Go on then, I’m sure you got work to do. Keep the flame lit please. Please keep it lit.");
                        writer.Close();
                    }
                }
            }
            //Depressed
            else if (moodState == 1)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]They have to be dead, it’s been too long now. At this rate though...............so will we.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Evelyn. Evelyn! Please, I’m begging you.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Whoa, what’s the matter Ms. Junifer?");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]I’ve never seen the flame get that dim. You can’t let it go out PLEASE. He will not be happy.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]No, don’t worry, everything will be alright.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer].........................");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]If he comes out, there’s nothing we’ll be able to do, you know, right?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]..........................");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]Please don’t let it go out, I’m begging you.");
                        writer.WriteLine("[NAME=Baericks Junifer][SPEAKERSPRITE=Baericks Junifer]I don’t wanna die.");
                        writer.Close();
                    }
                }
            }

        }
        //Gorgui's Dialogue
        else if (charName == "Gorgui Bayers")
        {
            //Happy
            if (moodState == 4)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Oh, I just remembered I have to prepare for the Safety Seminar. So much stuff to do……..I might have to skip shelter duties.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hello Mr. Bayers, how’s your day going?");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Ahhhh, nothing compared to my former days, young Evelyn. Those days of policing are behind me, I’m enjoying my more relaxed life.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]You volunteer at Brancia’s Cat Shelter now, don’t you?");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Yes, I do! Those tiny little critters are some troublemakers sometimes. Trying to herd them away from their litter boxes to clean them does a number on my frail bones!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Awwwww, I’m sure they make up for it with their cuteness though. I wish I could volunteer there if I had time.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Ohhh, your time will come eventually young Evelyn. You can find happiness in any job though, just gotta put your back into it.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Yeah, I’m trying. For now, there’s nothing too interesting about keeping the fire lit. But hey, at least it’s decently easy.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Well, it could be worse. Could be a hard job that you hate.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Oh, I forgot I have my juice tasting panel tomorrow. I have to start getting ready for that.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Take care young Evelyn. Make sure to stay out of trouble. You’re still very young after all!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Thank you Mr. Bayer, you too!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I mean the take care part, not the stay out of trouble part, whoops.");
                        writer.Close();
                    }
                }
            }
            //Moderate
            else if (moodState == 3)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]My knee is not taking this scratch well. I hope I don’t have an infection...");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, hello Mr. Bayers. How are things going at the shelter?");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Well, one of those troublemaking critters scratched me in my last shift. It was a bit deep, and I had to get my son to patch it up for me. Couldn’t do it as well as old Merilyn could, but he still got the job done.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Well that’s a bummer. They didn’t seem so aggressive when I last visited.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]It’s probably a new one, I’m never able to keep track of them these days. That one had a fiery temper though, I forgot to feed it while feeding the other cats and it probably got impatient.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]It’s just a scratch though, this old man has gone though far worse during my policing days.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]That’s the spirit!");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]They put that cat somewhere else though, probably just to calm it down. Although I don’t agree, it’s just a cat after all. They don’t know better when it comes to their blazing personalities.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Don’t worry, I’ll visit it one day and get it to calm down. I’d like to say I’m an excellent cat whisperer.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Don’t push your luck with that one though. She’s high on the list for troublemakers I’ve faced.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Well, I gotta go prepare some papers, I’m a little late on writing a presentation. Take care, young Evelyn.");
                        writer.Close();
                    }
                }
            }
            //Sad
            else if (moodState == 2)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Richard! Can you get my chair for me? I feel like I’m about to fall over.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Merilyn? Is that really you?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]No, it’s me, Evelyn.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Oh, sorry, it must be one of those days.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]I miss her sometimes. But that’s how life goes. Luckily you’re still so young. That fire job isn’t killing you, is it?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I’m doing fine, thanks for asking.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Good, it’s getting a bit chilly. My old bones are not what they used to be, so I’m not exactly taking it well. I should probably stay indoors.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Don’t worry, I’m working on it. It should be warm again soon.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Thank you young Evelyn.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Maybe I should skip the shelter for today, I can barely move with how cold it is.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Sorry Merilyn, I’m just not feeling it today...");
                        writer.Close();
                    }
                }
            }
            //Depressed
            else if (moodState == 1)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]So cold, so empty. So this is what you felt Merilyn. I’m.........so sorry.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]I can’t even stand anymore, that’s how bad it’s gotten, huh. RICHARD, get my notebook, will ya?");
                        writer.WriteLine("[NAME=Richard][SPEAKERSPRITE=Richard]Get it yourself old man, I’m busy with stuff.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Why you little- oh, hello Evelyn.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hi Mr. Bayers.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]I-I don’t think I can walk anymore, my time could be coming soon. How’s the flame doing?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Uhhhhhh, it’s under control, don’t worry about it.");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Well, hopefully things get better for me then...");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers].................");
                        writer.WriteLine("[NAME=Gorgui Bayers][SPEAKERSPRITE=Gorgui Bayers]Can you hear me Merilyn? We’ll be reunited soon.");
                        writer.Close();
                    }
                }
            }
        }
        //Ocetire's Dialogue
        else if (charName == "Ocetire Sakoru")
        {
            //Happy
            if (moodState == 4)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]This is a FANTASTIC batch, oh wow. Evelyn won’t be able to make any jokes about these cookies!");
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Well, if it isn’t our local stick and stones collector!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I'd roast you back, but I don’t want you to burn like your last batch of cookies.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Hey, that’s a bit much, hehehe.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]How goes lighting up the flames or whatever you do.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Oh, you know, the usual, go blah blah blah with my rituals, or whatnot. It’s so repetitive these days");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Sounds like someone is always lazing about and whining.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I get so much free time though. You wish you had as much as me.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Oh, womp womp. You wanna go get food together later today?");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]I’d be down! Should we go to Maurice’s place this time?");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Yeah, we should! I’ve heard good things about her place. Still can’t compete with my sweets though.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Let’s see about that. I’ll have your specialty then.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]One coming up lazy pyromancer!");
                        writer.Close();
                    }
                }
            }
            //Moderate
            else if (moodState == 3)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Wait, the inventory doesn’t seem right. EVELYN. GET BACK HERE.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Greetings and felicitations Ocetire Sakoru!");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]You and your silly speeches. Probably poured more time into that greeting than you did for keeping the fire lit today.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hey now, today has been a rough day for resource gathering you know? Is someone angry that their batch of flour hasn’t arrived yet?");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Wah wah wah. I just didn’t have my morning coffee, that’s it.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Yeah, me neither.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Want to help me make a batch of cookies? I’m sure your expertise in it is sooooo huge that you’ll make the perfect batch.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Nah, I’d lose. Lose time to work on materials gathering for today. I also don’t want to make you jealous.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]I was clearly being sarcastic girlie. You know better than anyone that throughout the villages and fires, I alone am the greatest baker.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Uh huh, sureeeeeee.");
                        writer.Close();
                    }
                }
            }
            //Sad
            else if (moodState == 2)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]I-I don’t wanna talk right now Evelyn, sorry.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Hey, are you really going to sell those batches of cookies? You know they looked like they survived a battle with a bear, right?");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Oh, come on! They weren’t that bad. Besides, I was experimenting! At least I’m good at baking, unlike Ms. HELP, MY KITCHEN IS BURNING!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Excuse me? My cooking is legendary! Everyone loves my food. You could never compete with my culinary expertise.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Culinary expertise? Is that what we’re calling it? Last time you tried to make cupcakes with me, they exploded in the oven. It was a disaster!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]At least I don’t burn water! Seriously, every time you step into the kitchen, it’s like a cooking horror movie. I’m surprised I haven’t been called over to put out your fire.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]You would know a lot about putting out fires, wouldn’t you? Look at the poor little fire, you’re literally putting it out!");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]As if you would know how the fire should look when it's healthy. Maybe if you spent a little more time practicing your cooking instead of getting addicted to coffee, you’d actually bake something edible!");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Or maybe I just don’t care what you think! How about you go work on putting your sticks into your pathetic fire. Maybe that’ll get you to shut up.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Fine! You know what? You can keep your awful cookies. I’ll make sure everyone knows who’s really more valuable for the village!");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Be that way! And I’ll make sure to remind everyone that you struggle with upkeeping a basic fire!");
                        writer.Close();
                    }
                }
            }
            //Depressed
            else if (moodState == 1)
            {
                if (short_text)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]................Go away Evelyn. Unless you want us all to die.");
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        writer.Flush();
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn].....");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Look who it is.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]What was the record again? 100 years without the fire getting that bad? Wow, what an amazing 100 year anniversary you’ve brought us.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Some pyromancer you are.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]Shut up, you’re not usually this chirpy.");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]How could I not be!? Look around Evelyn, everyone is miserable, all because you can’t keep a basic fire lit!");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Just quit the job if you’re not good at it. At least we’ll find someone who can actually keep it lit.");
                        writer.WriteLine("[NAME=Evelyn][SPEAKERSPRITE=Evelyn]JUST SHUT UP ALREADY!");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]....................");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]...");
                        writer.WriteLine("[NAME=Ocetire Sakoru][SPEAKERSPRITE=Ocetire Sakoru]Fine. Be that way. Go back to your resource gathering or whatever.");
                        writer.Close();
                    }
                }
            }
        }
        AssetDatabase.Refresh();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Collision");
        if (other.gameObject.tag == "Player" && !hasBeenUsed)
        {
            inArea = true;
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
        if (!NPC_Manager.short_interaction && in_dialogue)
        {
            NPC_Manager.UpdateShortInteraction();
        }
        if (other.gameObject.tag == "Player")
        {
            manager.EndDialogue();
            in_dialogue = false;
        }
        inArea = false;
    }
}
