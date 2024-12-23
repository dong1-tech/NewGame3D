using InventorySystem;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaving
{
    public void SaveData(List<InventoryItem> listOfInventoryItem)
    {
        Data data = new();
        foreach (var item in listOfInventoryItem)
        {
            data.itemID.Add(item.GetID());
            data.itemQuanity.Add(item.itemQuanity);
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "SaveData.json", json);
    }
}
public class DataLoading
{
    public void LoadData()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "SaveData.json");
        Data data = JsonUtility.FromJson<Data>(json);
        if (data == null) return;
        List<InventoryItem> listOfInventoryItem = new List<InventoryItem>();
        for (int i = 0; i < data.itemID.Count; i++)
        {
            InventoryItem newItem = new InventoryItem(data.itemID[i], data.itemQuanity[i]);
            listOfInventoryItem.Add(newItem);
        }
        PlayerData.instance.LoadData(listOfInventoryItem);
    }
}

