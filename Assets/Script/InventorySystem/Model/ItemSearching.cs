using System.Collections.Generic;
using UnityEngine;

namespace GameConfig
{
    public class ItemSearching : MonoBehaviour
    {
        private Dictionary<int, ItemSO> dictionaryOfAvaiableItem = new();

        public static ItemSearching instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            LoadFromResource();
        }

        private void LoadFromResource() 
        {
            Object[] loadObject = Resources.LoadAll("Data/ItemSO", typeof(ItemSO));
            foreach (var item in loadObject)
            {
                ItemSO itemSO = (ItemSO)item;
                dictionaryOfAvaiableItem.Add(itemSO.itemID, itemSO);
                Resources.UnloadAsset(item);
            }
        }

        public ItemSO GetItemFromID(int id)
        {
            return dictionaryOfAvaiableItem[id];
        }
    }
}
