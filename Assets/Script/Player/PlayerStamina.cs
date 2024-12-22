using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private float currentStamina;
    [SerializeField]
    private float maxStamina;
    private float recoveryAmount;

    private bool isFull;

    public Action<float> OnStaminaChange;

    private void Init()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if(currentStamina < maxStamina)
        {
            RecoveryStamina();
        }
    }

    public void OnDefendHit(float usedStamina)
    {
        if(usedStamina > currentStamina)
        {
            return;
        }
        currentStamina -= usedStamina;
        OnStaminaChange.Invoke(currentStamina / maxStamina);
    }

    public void OnAttack(float usedStamina)
    {
        if (usedStamina > currentStamina)
        {
            return;
        }
        currentStamina -= usedStamina;
        OnStaminaChange.Invoke(currentStamina / maxStamina);
    }
    
    public void RecoveryStamina()
    {
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
