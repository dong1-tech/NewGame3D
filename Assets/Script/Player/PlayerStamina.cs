using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private float currentStamina;
    [SerializeField]
    private float maxStamina;
    [SerializeField]
    private float recoveryAmount;
    [SerializeField]
    private float usedStaminaPerAttack;
    [SerializeField]
    private float usedStaminaPerDefend;

    public Action<float> OnStaminaChange;

    private void Awake()
    {
        currentStamina = maxStamina;
        InvokeRepeating("RecoveryStamina", 0, 0.5f);
    }

    public bool OnDefendHit()
    {
        if(usedStaminaPerDefend > currentStamina)
        {
            return false;
        }
        currentStamina -= usedStaminaPerDefend;
        OnStaminaChange.Invoke(currentStamina / maxStamina);
        return true;
    }

    public bool OnAttack()
    {
        if (usedStaminaPerAttack > currentStamina)
        {
            return false;
        }
        currentStamina -= usedStaminaPerAttack;
        OnStaminaChange.Invoke(currentStamina / maxStamina);
        return true;
    }
    
    private void RecoveryStamina()
    {
        if (currentStamina == maxStamina) return;
        if(currentStamina + recoveryAmount > maxStamina)
        {
            currentStamina = maxStamina;
        }
        else
        {
            currentStamina += recoveryAmount;
        }
        OnStaminaChange.Invoke(currentStamina/maxStamina);
    }
}
