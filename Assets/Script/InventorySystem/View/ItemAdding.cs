using System.Collections.Generic;

namespace InventorySystem
{
    public class ItemAdding
    {
        public void AddItemIntoInventory(int itemID, int quanity)
        {
            List<ItemUI> listOfUIItem = InventoyUIManager.instance.listOfItemUI;
            int pos = 0;
            int count = 0;
            for (int i = 0; i < listOfUIItem.Count; i++)
            {
                if (itemID == listOfUIItem[i].GetItemID())
                {
                    listOfUIItem[i].OnAddingItem(quanity);
                    return;
                }
                if (listOfUIItem[i].IsEmpty() && count == 0)
                {
                    pos = i;
                    count = 1;
                }
            }
            if (count != 0)
            {
                InventoryItem newItem1 = PlayerData.instance.AddItem(new InventoryItem(itemID, quanity));
                listOfUIItem[pos].SetData(itemID, quanity, newItem1.itemDataId);
                return;
            }
            int listCount = listOfUIItem.Count;
            InventoyUIManager.instance.SpawnItemUI(5);
            listOfUIItem = InventoyUIManager.instance.listOfItemUI;
            InventoryItem newItem2 = PlayerData.instance.AddItem(new InventoryItem(itemID, quanity));
            listOfUIItem[listCount].SetData(itemID, quanity, newItem2.itemDataId);
        }
    }
}
