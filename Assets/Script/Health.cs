using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    private float currentHealth;
    [SerializeField]
    private float maxHealth;
    public Action<float> OnHealthChange;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
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

}
