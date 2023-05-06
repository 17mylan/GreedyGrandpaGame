using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WaveCounter : MonoBehaviour
{
    private Animator animator;
    private TextMeshProUGUI currentKillsTMP;
    private TextMeshProUGUI mobsAmountTMP;

    
    /// <summary>
    /// Init all references variables.
    /// Animator
    /// Current Kills
    /// Mobs
    /// </summary>
    private void Awake()
    {
        if (TryGetComponent(out Animator anim)) animator = anim;
        if (transform.GetChild(0).TryGetComponent(out TextMeshProUGUI kills)) currentKillsTMP = kills;
        if (transform.GetChild(1).TryGetComponent(out TextMeshProUGUI mobs)) mobsAmountTMP = mobs;
    }

    public void ChangeText(int kills, int waveMobs)
    {
        currentKillsTMP.text = kills.ToString("00");
        mobsAmountTMP.text = waveMobs.ToString("/ 00");
        
        animator.SetTrigger("Changed");
    }
    
}
