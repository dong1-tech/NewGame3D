using InventorySystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<int, InventoryItem> inventoryItems = new();

    private int inventoryItemID = 10000;

    public static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadData(List<InventoryItem> listOfInventoryItem)
    {
        foreach (InventoryItem item in listOfInventoryItem)
        {
            item.itemDataId = inventoryItemID;
            inventoryItems.Add(item.itemDataId, item);
            inventoryItemID++;
        }
        InventoryController.instance.OnLoad(listOfInventoryItem);
    }

    public InventoryItem AddItem(InventoryItem item)
    {
        item.itemDataId = inventoryItemID;
        inventoryItems.Add(item.itemDataId, item);
        inventoryItemID++;
        return item;
    }

    public void SaveData()
    {
        List<InventoryItem> list = new List<InventoryItem>();
        foreach(KeyValuePair<int, InventoryItem> pair in inventoryItems)
        {
            list.Add(pair.Value);
        }
        DataSaving dataSaving = new DataSaving();
        dataSaving.SaveData(list);
    }

    public void OnUseItem(int inventoryItemID)
    {
        InventoryItem item = inventoryItems[inventoryItemID];
        item.itemQuanity -= 1;
        GameManager.Instance.NotifyOnUseItem(item.GetID());
    }
}
