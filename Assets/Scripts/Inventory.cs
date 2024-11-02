using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public bool Add (Resource resource)
    { 
        if(resources.Count >= space)
        {
            Debug.Log("Out of space");
            return false;
        }
        resources.Add(resource);
        if(onItemChangedCallBack !=null)
        {
            onItemChangedCallBack.Invoke();
        }
        

        return true;
    }

    public void Remove (Resource resource)
    {
        resources.Remove(resource);
    }
}
