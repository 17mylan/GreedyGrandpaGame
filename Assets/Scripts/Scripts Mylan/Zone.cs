using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public GameObject InteractionMessage;
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PlayerRotation")) 
        {
            InteractionMessage.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("PlayerRotation")) 
        {
            InteractionMessage.SetActive(false);
        }
    }
}
