using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseMenu : MonoBehaviour
{
    #region Actions/Events

    public void ActionQuitGame()
    {
        GameManager.QuitGame();
    }

    public void ActionResume()
    {
        GameManager.instance.PauseGame(false);
    }
    
    public void ActionBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    #endregion
    
    
}
