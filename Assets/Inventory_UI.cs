using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    
    Inventory inventory;

    InventorySlot[] slots;
     
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.N))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }  
    }

   void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++) 
        {
            if (i < inventory.resources.Count)
            {
                slots[i].AddItem(inventory.resources[i]);
            }
            else
            {
                slots[i].ClearSlot(); 
            }
        }
    }



}
