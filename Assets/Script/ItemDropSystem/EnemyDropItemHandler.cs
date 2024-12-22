using GameConfig;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItemHandler
{
    public List<ItemSO> OnDropItem(int enemyDropItemID)
    {
        EnemiesSO newEnemyItemDrop = ItemDropConfigSearching.instance.
            GetItemDropThroughID(enemyDropItemID);
        int numberOfReward = (int)newEnemyItemDrop.GetEnemyTier();
        List<ItemSO> possibleReward = newEnemyItemDrop.GetListOfDroppableItem();
        float itemValue = Random.Range(1f, 100f);
        List<ItemSO> listOfPrize = new();
        foreach(var item in possibleReward)
        {
            if (itemValue < (int)item.itemTier)
            {
                listOfPrize.Add(item);
            }
        }
        if(listOfPrize.Count == 0)
        {
            return null;
        }
        if(listOfPrize.Count <= numberOfReward)
        {
            return listOfPrize;
        }
        if(listOfPrize.Count > numberOfReward)
        {
            for(int i = 0; i < listOfPrize.Count - numberOfReward; i++)
            {
                listOfPrize.Remove(listOfPrize[Random.Range(0, listOfPrize.Count)]);
            }
            return listOfPrize;
        }
        return null;
    }
}
