using System.Collections.Generic;

namespace InventorySystem
{
    public class Data
    {
        public List<int> itemID = new();
        public List<int> itemQuanity = new();
    }

    public class InventoryItem
    {
        public int itemDataId; // 
        private int itemID; // config id
        public int itemQuanity;

        public InventoryItem(int itemID, int itemQuanity)
        {
            this.itemID = itemID;
            this.itemQuanity = itemQuanity;
        }

        public int GetID()
        {
            return itemID;
        }
    }
}


