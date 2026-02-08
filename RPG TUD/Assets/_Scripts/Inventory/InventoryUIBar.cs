using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryUIBar : MonoBehaviour
{
    private GameObject[] itemSlot;
    private List<Item> inventoryList;
    private InventoryManager inventoryActionScripts;

    private void OnEnable()
    {
        InventoryManager.OnInventoryChange += UpdateUIBar;
    }
    private void OnDisable()
    {
        InventoryManager.OnInventoryChange -= UpdateUIBar;
    }


    void Start()
    {
        itemSlot = new GameObject[3];
        inventoryList = new List<Item>();

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = transform.GetChild(i).gameObject;
        }

        inventoryActionScripts = FindFirstObjectByType<InventoryManager>().GetComponent<InventoryManager>();
    }

   

    void UpdateUIBar()
    {
        inventoryList = inventoryActionScripts.GetIventoryList();

        for (int i = 0; i < inventoryList.Count; i++)
        {
            itemSlot[i].gameObject.SetActive(true);
            itemSlot[i].transform.GetChild(2).gameObject.SetActive(true);
            itemSlot[i].transform.GetChild(2).GetComponent<Image>().sprite = inventoryList[i].itemIcon;
            itemSlot[i].transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                inventoryList[i].stackSize.ToString();
        }
    }
}
