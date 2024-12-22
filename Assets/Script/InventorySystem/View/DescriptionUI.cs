using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class DescriptionUI : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text itemDescription;

        public void DisplayItemInfo(ItemUI itemUI)
        {
            ItemSO itemSO = InventoyUIManager.instance.GetItemFromID(itemUI.GetItemID());
            itemImage.sprite = itemSO.ItemImage;
            itemDescription.text = itemSO.ItemDescription;
        }
    }
}
