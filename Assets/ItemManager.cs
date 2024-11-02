using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject slot;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left Click");
        }
    }

    public void DropItem()
    {
       InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
       inventorySlot.OnRemoveButton();
    }
}
