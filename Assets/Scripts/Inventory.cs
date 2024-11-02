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
            Debug.Log("Uh oh, more than one inventory");
        }
        instance = this; 
    }
    //End Singleton

    public int space = 10;

    public List<Resource> resources = new List<Resource>();

    public void Add (Resource resource)
    {
        if(resources.Count >= space)
        {
            Debug.Log("Out of space");
            return;
        }
        resources.Add(resource);
    }

    public void Remove (Resource resource)
    {
        resources.Remove(resource);
    }
}
