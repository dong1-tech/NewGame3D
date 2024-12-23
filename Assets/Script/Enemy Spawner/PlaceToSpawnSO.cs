
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnConfig
{
    public GameObject prefab;
    public int number;
}


[CreateAssetMenu]
public class PlaceToSpawnSO : ScriptableObject
{
    [SerializeField]
    private Vector3 spawnPosition;
    public Vector3 SpawmPosition { get { return spawnPosition; } }

   // List< EnemySpawnConfig>


    [SerializeField]
    private List<Enemy> enemies;
    public List<Enemy> Enemies { get { return enemies; } }
    [SerializeField]
    private List<int> numberOfEachEnemy;   
    public List<int> NumberOfEachEnemy { get { return numberOfEachEnemy; } }
}
