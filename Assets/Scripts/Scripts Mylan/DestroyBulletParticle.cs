using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBulletParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4f);
    }
}
