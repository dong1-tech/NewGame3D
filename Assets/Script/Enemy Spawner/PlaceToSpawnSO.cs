
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlaceToSpawnSO : ScriptableObject
{
    [SerializeField]
    private Vector3 spawnPosition;
    public Vector3 SpawmPosition { get { return spawnPosition; } }   
    [SerializeField]
    private List<Enemy> enemies;
    public List<Enemy> Enemies { get { return enemies; } }
    [SerializeField]
    private List<int> numberOfEachEnemy;   
    public List<int> NumberOfEachEnemy { get { return numberOfEachEnemy; } }
}
