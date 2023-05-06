using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject fadeToBlackImage;
    public string sceneName;
    public void Play()
    {
        SceneManager.LoadScene("Cinematique_Principale");
        //StartCoroutine(fadeToBlack());
        //print("1");
    }

    /*IEnumerator fadeToBlack()
    {
        fadeToBlackImage.SetActive(true);
        print("2");
        yield return new WaitForSeconds(1f);
        print("3");
        SceneManager.LoadScene("Cinematique_Principale");
    }*/
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        GameManager.QuitGame();
    }

    public void Alien()
    {
        SceneManager.LoadScene(3);
    }
}