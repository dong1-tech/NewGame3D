using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image healthBar;

    private void Awake()
    {
        healthBar = GetComponent<Image>();
        healthBar.fillAmount = 1;
    }

    public void OnValueChange(float newValue)
    {
        healthBar.fillAmount = newValue;
    }
}
