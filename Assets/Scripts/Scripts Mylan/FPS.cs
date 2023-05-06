using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        int roundedFPS = Mathf.RoundToInt(fps);
        if (roundedFPS <= 30)
        {
            fpsText.color = Color.red;
        }
        else if (roundedFPS > 31 && roundedFPS <= 59)
        {
            fpsText.color = Color.yellow;
        }
        else if (roundedFPS >= 60)
        {
            fpsText.color = Color.green;
        }
        fpsText.text =  roundedFPS.ToString();
    }
}
