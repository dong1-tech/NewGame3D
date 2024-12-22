using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoyUIManager : MonoBehaviour
    {
        [SerializeField]
        private DescriptionUI descriptionUI;

        [SerializeField]
        private InventoryController inventoryController;

        [SerializeField]
        private ItemUISpawner itemUISpawner;

        public static InventoyUIManager instance;

        [HideInInspector]
        public List<ItemUI> listOfItemUI;

        private ItemUI currentItemUI;

        private int clickCount;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            listOfItemUI = new();
        }

        public ItemSO GetItemFromID(int id)
        {
            return inventoryController.GetItemFromID(id);
        }

        public void SpawnItemUI(int numberOfSlot)
        {
            itemUISpawner.SpawnItemUI(numberOfSlot);
        }

        public void OnSelectItemUI(ItemUI itemUI)
        {
            descriptionUI.DisplayItemInfo(itemUI);

            if (currentItemUI == null)
            {
                currentItemUI = itemUI;
                clickCount = 1;
                return;
            }

            if (currentItemUI != itemUI)
            {
                currentItemUI.TurnOffBorder();
                currentItemUI.TurnOffUsingItemPanel();
                currentItemUI = itemUI;
                clickCount = 1;
            }
            else
            {
                clickCount = 2;
            }

            if (clickCount == 2)
            {
                itemUI.TurnOnUsingItemPanel();
            }
        }

        public void UsingItem(int inventoryItemID)
        {
            inventoryController.UsingItem(inventoryItemID);
        }

        public void LoadData(List<InventoryItem> listOfInventoryItem)
        {
            ClearOldData();
            instance.SpawnItemUI(listOfInventoryItem.Count + 5);
            for (int i = 0; i < listOfInventoryItem.Count; i++)
            {
                listOfItemUI[i].SetData(listOfInventoryItem[i].GetID(),
                    listOfInventoryItem[i].itemQuanity, listOfInventoryItem[i].itemDataId);
            }
        }

        private void ClearOldData()
        {
            foreach (var item in listOfItemUI)
            {
                item.DestroyObject();
            }
            listOfItemUI.Clear();
        }
    }
}
