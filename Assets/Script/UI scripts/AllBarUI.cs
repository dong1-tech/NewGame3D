using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllBarUI : MonoBehaviour
{
    private Image bar;

    private void Awake()
    {
        bar = GetComponent<Image>();
        bar.fillAmount = 1;
    }

    public void OnValueChange(float newValue)
    {
        bar.fillAmount = newValue;
    }
}
