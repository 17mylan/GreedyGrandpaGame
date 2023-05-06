using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGrenade : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource.Play();
        Destroy(gameObject, 2f);
    }
}
