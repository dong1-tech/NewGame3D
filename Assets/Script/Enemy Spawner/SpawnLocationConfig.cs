using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameConfig
{
    public class SpawnLocationConfig : MonoBehaviour
    {
        private List<PlaceToSpawnSO> placeToSpawnSOs = new();

        [SerializeField]
        private Transform playerPos;
        [SerializeField]
        private Camera cam;

        private void Awake()
        {
            LoadFromResources();
            InvokeRepeating("CheckDistance", 1, 3);
        }

        private void LoadFromResources()
        {
            Object[] loadObject = Resources.LoadAll("Data/PlaceToSpawn", typeof(PlaceToSpawnSO));
            foreach (var place in loadObject)
            {
                PlaceToSpawnSO newPlace = (PlaceToSpawnSO)place;
                placeToSpawnSOs.Add(newPlace);
                Resources.UnloadAsset(newPlace);
            }
        }

        private void CheckDistance()
        {
            foreach (var place in placeToSpawnSOs.ToList())
            {
                float distance = Vector3.Distance(playerPos.position, place.SpawmPosition);
                if (distance < 50)
                {
                    placeToSpawnSOs.Remove(place);
                    SpawnEnemy(place);
                }
            }
        }

        private void SpawnEnemy(PlaceToSpawnSO places)
        {

            for (int i = 0; i < places.enemySpawnConfigs.Count; i++)
            {
                for(int j = 0; j < places.enemySpawnConfigs[i].number; j++)
                {
                    Vector2 random = Random.insideUnitCircle * 2;
                    Enemy spawnEnemy = Instantiate(places.enemySpawnConfigs[i].prefab,
                        new Vector3(places.SpawmPosition.x + random.x, places.SpawmPosition.y, places.SpawmPosition.z + random.y), Quaternion.identity);
                    spawnEnemy.SetUp(playerPos, cam);
                }
            }
        }
    }
}
