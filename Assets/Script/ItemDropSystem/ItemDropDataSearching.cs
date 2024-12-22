using System.Collections.Generic;
using UnityEngine;

namespace GameConfig
{
    public class ItemDropConfigSearching : MonoBehaviour
    {
        private Dictionary<int, EnemiesSO> dictionaryOfEnemyItemDrop = new();

        public static ItemDropConfigSearching instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            LoadFromResource();
        }

        private void LoadFromResource()
        {
            Object[] loadObject = Resources.LoadAll("Data/ItemDropSO", typeof(EnemiesSO));
            foreach (var item in loadObject)
            {
                EnemiesSO enemyItemDrop = (EnemiesSO)item;
                dictionaryOfEnemyItemDrop.Add(enemyItemDrop.GetEnemyItemDropID(), enemyItemDrop);
                Resources.UnloadAsset(item);
            }
        }

        public EnemiesSO GetItemDropThroughID(int id)
        {
            return dictionaryOfEnemyItemDrop[id];
        }
    }
}
