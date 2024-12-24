using UnityEngine;

public class PlayerBody : MonoBehaviour, IHitable
{
    [SerializeField] private PlayerController controller;

    private Health playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void OnHit(float damageRecieve)
    {
        if (!playerHealth.IsDead())
        {
            controller.HandlerOnHit();
            playerHealth.TakeDamage(damageRecieve);
        }
    }
}
