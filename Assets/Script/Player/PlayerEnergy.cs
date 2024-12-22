using System;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private float currentEnergy;
    [SerializeField]
    private float maxEnergy;
    public Action<float> OnManaChange;

    private void Init()
    {
        currentEnergy = maxEnergy;
    }

    public void UseSpell(float usedEnergy)
    {
        if(currentEnergy < usedEnergy)
        {
            return;
        }
        currentEnergy -= usedEnergy;
        
        OnManaChange.Invoke(currentEnergy / maxEnergy);
    }

    public void RecoveryEnergy(float energyAmount)
    {
        if (currentEnergy + energyAmount > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy += energyAmount;
        }
        OnManaChange.Invoke(currentEnergy / maxEnergy);
    }
}
