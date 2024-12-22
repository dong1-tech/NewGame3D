using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    private Transform enemyPos;

    [SerializeField]
    private Image healthBar;

    private Camera cam;

    private void Update()
    {
        HealthBarFollowEnemy();
    }

    private void HealthBarFollowEnemy()
    {
        transform.position = enemyPos.position;
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public void SetUp(Transform enemyPos, Camera cam)
    {
        this.enemyPos = enemyPos;
        this.cam = cam;
    }

    public void UpdateHealthBar(float currentHealthPercent)
    {
        healthBar.fillAmount = currentHealthPercent;
    }
}
