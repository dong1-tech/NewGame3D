using GameConfig;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryController: MonoBehaviour
    {
        private ItemAdding itemAdding;

        public static InventoryController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void OnLoad(List<InventoryItem> listOfInventoryItem)
        {
            InventoyUIManager.instance.LoadData(listOfInventoryItem);
        }

        public void AddItem(InventoryItem item)
        {
            itemAdding = new();
            itemAdding.AddItemIntoInventory(item.GetID(), item.itemQuanity); 
        }

        public void UsingItem(int inventoryItemID)
        {
            PlayerData.instance.OnUseItem(inventoryItemID);
        }

        public ItemSO GetItemFromID(int id)
        {
            return ItemSearching.instance.GetItemFromID(id);
        }
    }
}
