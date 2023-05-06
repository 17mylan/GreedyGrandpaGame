using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager instance { private set; get; }

    private const KeyCode pause = KeyCode.Mouse2;
    private bool isPaused;

    [HideInInspector] public int money;
    

    

    #endregion

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            instance = this;
        }

        instance = this;
    }

    /// <summary>
    /// Check if Escape is down to open the pause menu.
    /// </summary>
    void Update()
    {
        // Debug.Log(money);
        if (Input.GetKeyDown(pause))
        {
            PauseGame(!isPaused);
        }
        
        
    }
    public void UpdateMoneyHUD()
    {
        UIManager.instance.UpdateDollarHUD();
    }
    public static void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(bool value)
    {
        isPaused = value;
        Time.timeScale = isPaused ? 0f : 1f;

        UIManager.instance.HUD.SetActive(!value);
        UIManager.instance.PauseMenu.SetActive(value);
    }
    
    
    
    
}
