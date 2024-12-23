
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnConfig
{
    public Enemy prefab;
    public int number;
}


[CreateAssetMenu]
public class PlaceToSpawnSO : ScriptableObject
{
    [SerializeField]
    private Vector3 spawnPosition;
    public Vector3 SpawmPosition { get { return spawnPosition; } }
    public List<EnemySpawnConfig> enemySpawnConfigs;
}
