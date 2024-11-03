using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Resource resource;
    public Button itemButton;
    

    public void AddItem(Resource newItem)
    {
        resource = newItem;
        icon.sprite = resource.icon;
        icon.enabled = true;
        itemButton.interactable = true;
    }

    public void ClearSlot() 
    {
        resource = null;
        icon.sprite = null; 
        icon.enabled = false;
        itemButton.interactable = false;
        
    } 

    public void OnRemoveButton()
    {
        Debug.Log("dropped");
        Inventory.instance.Remove(resource);
    }
}
