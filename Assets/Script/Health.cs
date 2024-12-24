using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    protected float currentHealth;
    [SerializeField]
    protected float maxHealth;
    public Action<float> OnHealthChange;
    protected bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            isDead = true;
            GetComponentInParent<IDeadable>().OnDead();
        }
        OnHealthChange.Invoke(currentHealth / maxHealth);
    }

    public void Heal(float healAmount)
    {
        if(currentHealth + healAmount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healAmount;
        }
        OnHealthChange.Invoke(currentHealth / maxHealth);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
