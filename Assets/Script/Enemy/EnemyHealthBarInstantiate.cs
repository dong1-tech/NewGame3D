using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBarPrefab;

    public EnemyHealthBarUI InstantiateEnemyHealthBar(Transform followPos, Camera cam)
    {
        GameObject enemyHealthBar = Instantiate(healthBarPrefab);
        enemyHealthBar.transform.SetParent(transform);
        EnemyHealthBarUI newHealthBar = enemyHealthBar.GetComponent<EnemyHealthBarUI>();
        if (newHealthBar != null)
        {
            newHealthBar.SetUp(followPos, cam);
        }
        return newHealthBar;
    }
}
