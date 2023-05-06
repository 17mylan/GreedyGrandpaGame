using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI gameOverText;
    void Start()
    {
        gameOverText.text = "YOU SURVIVED " + PlayerPrefs.GetInt("Wave") + " WAVE(S)".ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
