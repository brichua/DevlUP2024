using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ritual", menuName = "Inventory/Ritual")]
public class Ritual : ScriptableObject
{
    new public string name = "New Ritual";
    public List<Resource> reqResources = new List<Resource>();
}
