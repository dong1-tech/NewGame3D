using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesSO : ScriptableObject
{
    [SerializeField]
    private int enemyID;

    [SerializeField]
    private List<ItemSO> listOfDroppableItem;

    [SerializeField]
    private EnemyTier tier;

    public int GetEnemyItemDropID()
    {
        return enemyID;
    }

    public List<ItemSO> GetListOfDroppableItem()
    {
        return listOfDroppableItem;
    }

    public EnemyTier GetEnemyTier()
    {
        return tier;
    }
}

public enum EnemyTier
{
    tierC = 1, tierB = 2, tierA = 3, tierS = 4
}

