using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    #region Variables

    private Slider healthBar;

    #endregion

    private void Awake()
    {
        if (TryGetComponent(out Slider slider)) healthBar = slider;
    }

    public void ChangeValueHealthBar(float currentHP, float maxHP)
    {
        
        healthBar.value = currentHP / maxHP;

    }
    
}
