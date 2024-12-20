using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Resource resource; 
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    { 
        Debug.Log("Picking Up " + resource.name);
        bool wasPickedUp = Inventory.instance.Add(resource);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
