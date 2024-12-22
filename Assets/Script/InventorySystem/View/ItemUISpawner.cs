using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class ItemUISpawner : MonoBehaviour
    {
        [SerializeField]
        private ItemUI itemUI;

        [SerializeField]
        private RectTransform contentPanel;

        public void SpawnItemUI(int inventorySize)
        {
            List<ItemUI> list = new List<ItemUI>();
            for (int i = 0; i < inventorySize; i++)
            {
                ItemUI newUIItem = Instantiate(itemUI);
                newUIItem.transform.SetParent(contentPanel);
                InventoyUIManager.instance.listOfItemUI.Add(newUIItem);
                list.Add(newUIItem);
            }
        }
    }
}
