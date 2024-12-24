using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public void RevivePlayer()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
