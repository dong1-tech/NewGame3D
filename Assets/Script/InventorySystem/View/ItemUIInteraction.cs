using UnityEngine;

namespace InventorySystem
{
    public class ItemUIInteraction : MonoBehaviour
    {
        [SerializeField]
        private ItemUI itemUI;

        public void OnItemClick()
        {
            if (itemUI.IsEmpty())
            {
                return;
            }
            itemUI.TurnOnBorder();
        }
    }
}
