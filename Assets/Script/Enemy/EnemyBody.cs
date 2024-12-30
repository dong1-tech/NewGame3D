using UnityEngine;

public class EnemyBody : MonoBehaviour, IHitable
{
    private Enemy enemy;
    private float timeToNextHit = 0f;
    private Health enemyHealth;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyHealth = GetComponent<Health>();
    }

    public void OnHit(float damageRecieve)
    {
        if (Time.time - timeToNextHit < 0.5f)
        {
            return;
        }
        timeToNextHit = Time.time;
        if (!enemyHealth.IsDead())
        {
            enemy.HandlerOnHit();
            enemyHealth.TakeDamage(damageRecieve);
        }
    }
}
