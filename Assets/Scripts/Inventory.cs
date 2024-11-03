using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    //Start Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Uh oh, more than one inventory");
            return;
        }
        instance = this; 
    }
    //End Singleton

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallBack;

    public int space = 10;

    public List<Resource> resources = new List<Resource>();

    public TextMeshProUGUI infoText;
    
    

    public bool Add (Resource resource)
    { 
        if(resources.Count >= space)
        {
            Debug.Log("Out of space");
            return false;
        }
        resources.Add(resource);
        //StartCoroutine(updateInfo(resource.name));
        statUpdate(resource,1);
        if(onItemChangedCallBack !=null)
        {
            onItemChangedCallBack.Invoke();
        }
        

        return true;
    }

    public void Remove (Resource resource)
    {
        resources.Remove(resource);
        statUpdate(resource, -1);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public void statUpdate(Resource resource, int change)
    {
       
            switch (resource.name)
            {
            case "Acacia Wood":
                Player.pinkWood+=change;
                break;
            case "Mystic Wood":
                Player.blueWood += change;
                break;
            case "Oak Wood":
                Player.brownWood += change;
                break;
            case "Suspicious Wood":
                Player.purpleWood += change;
                break;
            case "Spruce Wood":
                Player.yellowWood += change;
                break;
            case "Red Gem":
                Player.redGem += change;
                break;
            case "Green Gem":
                Player.greenGem += change;
                break;
            case "Yellow Gem":
                Player.yellowGem += change;
                break;
            case "Charm":
                Player.charm += change;
                break;
            case "Flower":
                Player.flower += change;
                break;
            case "Herb":
                Player.herb += change;
                break;
            case "Mossy Stone":
                Player.mossyStone += change;
                break;
            case "Stone":
                Player.stone += change;
                break;
            default:
                Debug.Log("Uh oh");
                break;
             

        }
        
        
    }

    private IEnumerator updateInfo(string text)
    {
        
        Debug.Log(infoText.text);
    
        infoText.text = "Picked up " + text;
        yield return new WaitForSeconds(2);
        infoText.text = "Press [N] to Open Inventory";
    }
}
/*
 * public static int pinkWood;
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
    public static int mossyStone;*/