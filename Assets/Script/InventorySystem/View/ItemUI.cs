using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField]
        private Image itemUIImage;

        [SerializeField]
        private TMP_Text itemUIQuanity;

        [SerializeField]
        private Image border;

        [SerializeField]
        private GameObject usingItemPanel;
        
        private bool isEmpty = true;
        private int itemQuanity;
        private int itemID;
        public int inventoryItemID;

        private void Awake()
        {
            itemUIImage.gameObject.SetActive(false);
            border.gameObject.SetActive(false);
            usingItemPanel.gameObject.SetActive(false);
        }

        public void SetData(int itemID, int itemQuanity, int inventoryItemID)
        {
            this.inventoryItemID = inventoryItemID;
            this.itemID = itemID;
            ItemSO newItemSO = InventoyUIManager.instance.GetItemFromID(itemID);
            itemUIImage.gameObject.SetActive(true);
            itemUIImage.sprite = newItemSO.ItemImage;
            isEmpty = false;
            this.itemQuanity += itemQuanity;
            itemUIQuanity.text = this.itemQuanity.ToString();
        }

        private void UpdateData()
        {
            itemUIQuanity.text = itemQuanity.ToString();
        }

        public int GetItemID()
        {
            return itemID;
        }

        public int GetItemQuanity()
        {
            return itemQuanity;
        }

        public bool IsEmpty()
        {
            return isEmpty;
        }

        public void TurnOffBorder()
        {
            border.gameObject.SetActive(false);
        }

        public void TurnOnBorder()
        {
            if (!border.isActiveAndEnabled)
            {
                border.gameObject.SetActive(true);
            }
            InventoyUIManager.instance.OnSelectItemUI(this);
        }

        public void TurnOnUsingItemPanel()
        {
            usingItemPanel.gameObject.SetActive(true);
        }

        public void TurnOffUsingItemPanel()
        {
            usingItemPanel.gameObject.SetActive(false);
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }

        public void OnAddingItem(int quanity)
        {
            itemQuanity += quanity;
            UpdateData();
            PlayerData.instance.inventoryItems[inventoryItemID].itemQuanity = itemQuanity;
        }

        public void OnUsingItem()
        {
            itemQuanity -= 1;
            if(itemQuanity <= 0)
            {
                InventoyUIManager.instance.UsingItem(inventoryItemID);
                PlayerData.instance.inventoryItems.Remove(inventoryItemID);
                DestroyObject();
                return;
            }
            UpdateData();
            PlayerData.instance.inventoryItems[inventoryItemID].itemQuanity = itemQuanity;
            InventoyUIManager.instance.UsingItem(inventoryItemID);
        }
    }
}
