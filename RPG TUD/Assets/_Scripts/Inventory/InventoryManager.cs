using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> inventoryList;

    // Create delegate function
    public static Action OnInventoryChange;


    void Start()
    {
        inventoryList = new List<Item>();
    }

    public List<Item> GetIventoryList()
    {
        return inventoryList;
    }

    public void AddItem(Item item)
    {
        // first item
        if(inventoryList.Count == 0)
        {
            inventoryList.Add(item);
        }
        else
        {
            bool inList = false;

            foreach (Item i in inventoryList)
            {
                if(item.itemName == i.itemName)
                {
                    i.stackSize++;
                    inList = true;
                }
            }

            if(!inList)
            {
                inventoryList.Add(item);
            }          
        }

        if (OnInventoryChange != null)
            OnInventoryChange();
    }



   
}
