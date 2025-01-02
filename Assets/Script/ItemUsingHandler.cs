using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsingHandler
{
    public void ClassifyItem(ItemTag itemTag, ItemSize itemSize)
    {
        switch(itemTag)
        {
            case ItemTag.Health:
                HealthPotionUsing(itemSize);
                break;
            case ItemTag.Mana:
                ManaPotionUsing(itemSize);
                break;
        }
    }

    private void ManaPotionUsing(ItemSize itemSize)
    {
        //
    }

    private void HealthPotionUsing(ItemSize itemSize)
    {
        Debug.Log(itemSize);
        Debug.Log((int) itemSize);
        GameManager.Instance.HealPlayer((int) itemSize);
    }
}
