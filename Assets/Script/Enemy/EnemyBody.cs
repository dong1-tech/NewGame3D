using UnityEngine;

public class EnemyBody : MonoBehaviour, IHitable
{
    [SerializeField]
    private Enemy enemy;

    private Health enemyHealth;

    void Awake()
    {
        enemyHealth = GetComponent<Health>();
    }

    public void OnHit(float damageRecieve)
    {
        enemy.HandlerOnHit();
        enemyHealth.TakeDamage(damageRecieve);
    }
}
