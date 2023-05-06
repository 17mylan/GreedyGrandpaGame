using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToWhiteScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(fadeToWhite());
    }
    IEnumerator fadeToWhite()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
