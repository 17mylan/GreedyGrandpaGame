using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DestroySelf : MonoBehaviour
{
    private Animator animator;
    
    private void Awake()
    {
        if (TryGetComponent(out Animator anim)) animator = anim;
    }

    public void DestroySelf()
    {
        this.gameObject.SetActive(false);
    }
}
