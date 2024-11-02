using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resource")]
public class Resource : ScriptableObject
{
    new public string name = "New Resource";
    public Sprite icon = null;
    public float burnTime = 100f;
     
    
}
