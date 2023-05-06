using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Overheating : MonoBehaviour
{
    #region Variables

    private Slider warningBar;
    private Animator animator;
    
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        if (UIManager.instance.warningBarGO.TryGetComponent(out Slider slider)) warningBar = slider;
        if (TryGetComponent(out Animator anim)) animator = anim;
    }

    public void UpdateWarningBar(float warningLevel, float maxWarningLevel)
    {
        warningBar.value = warningLevel / maxWarningLevel;

        if (warningLevel >= maxWarningLevel)
        {
            animator.SetTrigger("Warning");
        }
        // print( warningLevel / maxWarningLevel);
    }
    
}
