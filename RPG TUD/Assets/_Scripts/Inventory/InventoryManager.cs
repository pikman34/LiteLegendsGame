using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> inventoryList;

    public static InventoryManager Instance { get; private set; }
    Dictionary<int, int> itemsCountCache = new();
    public event Action OnInventoryChanged;
    // Create delegate function
    public static Action OnInventoryChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        inventoryList = new List<Item>();
        RebuildItemCounts();
    }

    public void RebuildItemCounts()
    {
        itemsCountCache.Clear();

        foreach (Item item in inventoryList)
        {
            if (item != null)
            {
                itemsCountCache[item.ID] = itemsCountCache.GetValueOrDefault(item.ID, 0) + item.stackSize;
            }
        }

        OnInventoryChanged?.Invoke();
    }

    public Dictionary<int, int> GetItemCounts() => itemsCountCache;

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
            RebuildItemCounts();
        }
        else
        {
            bool inList = false;

            foreach (Item i in inventoryList)
            {
                if(item.itemName == i.itemName)
                {
                    i.stackSize++;
                    RebuildItemCounts();
                    inList = true;
                }
            }

            if(!inList)
            {
                inventoryList.Add(item);
                RebuildItemCounts();
            }          
        }

        if (OnInventoryChange != null)
            OnInventoryChange();
    }

    public void RemoveItemsFromInventory(int itemID, int amountToRemove)
    {
        foreach(Item item in inventoryList)
        {
            if (amountToRemove <= 0) break;

            if(item.ID == itemID)
            {
                int removed = Mathf.Min(amountToRemove, item.stackSize);
                inventoryList.Remove(item);
                amountToRemove -= removed;

                if(item.stackSize == 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        RebuildItemCounts();
    }
}
