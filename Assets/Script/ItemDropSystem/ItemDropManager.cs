using InventorySystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [SerializeField]

    private TMP_Text rewardText;

    [SerializeField]
    private ItemDropMessageUI messageUI;

    public void OnDropItem(int enemyDropItemID)
    {
        EnemyDropItemHandler handler = new EnemyDropItemHandler();
        List<ItemSO> prizes = handler.OnDropItem(enemyDropItemID);
        if (prizes == null) return;
        string rewardText = "You got";
        foreach (var item in prizes)
        {
            InventoryController.instance.AddItem(new InventoryItem(item.itemID, 1));
            rewardText = rewardText + " " + item.itemName + "x1 ";
        }
        this.rewardText.text = rewardText;
        messageUI.Show();
    }
}
