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

        private void SpawnEnemy(PlaceToSpawnSO place)
        {
            for (int i = 0; i < place.Enemies.Count; i++)
            {
                for (int j = 0; j < place.NumberOfEachEnemy[i]; j++)
                {
                    Vector2 random = Random.insideUnitCircle * 2;
                    Enemy spawnEnemy = Instantiate(place.Enemies[i],
                        new Vector3(place.SpawmPosition.x + random.x, place.SpawmPosition.y, place.SpawmPosition.z + random.y), Quaternion.identity);
                    spawnEnemy.SetUp(playerPos, cam);
                }
            }
        }
    }
}
