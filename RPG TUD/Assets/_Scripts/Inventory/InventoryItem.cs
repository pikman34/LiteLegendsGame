using UnityEngine;

// To be added to objects that can be added to the inventory
public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public Item item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item = new Item();
        item.itemName = itemName;
        item.itemIcon = itemIcon;
    }

    public Item GetItem()
    {
        return item;
    }
}
