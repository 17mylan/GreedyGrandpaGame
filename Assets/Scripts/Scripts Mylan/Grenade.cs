using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EZCameraShake;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffectPrefab, explosionSound;
    public float explosionDelay = 2f;
    private float m_timeStamp;
    public List<GameObject> colliders = new List<GameObject>();
    public float explosionRadius = 30f;
    private SphereCollider sphereCollider;
    public bool canPlayEffect = true;
    void Start()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        sphereCollider.radius = explosionRadius;
        m_timeStamp = Time.time + explosionDelay;
    }

    void Update()
    {
        if (Time.time >= m_timeStamp)
        {
            Explode();
        }
    }
    IEnumerator effectExplosion()
    {
        canPlayEffect = false;
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Instantiate(explosionSound, transform.position, transform.rotation);
        CameraShaker.Instance.ShakeOnce(6f, 6f, .1f, 1f);
        yield return new WaitForSeconds(2.5f);
        canPlayEffect = true;
    }
    void Explode()
    {
        if(canPlayEffect)
            StartCoroutine(effectExplosion());
        foreach (var enemy in colliders.ToArray())
        {
            if (enemy == null)
            {
                continue;
            }
            else
            {
                
                var stateMachineAI = enemy.GetComponent<StateMachineAI>();
                colliders.Remove(enemy);
                stateMachineAI.TakeDamage(100f);
            }

        }
        colliders.Clear();

        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            colliders.Add(other.gameObject);
        }
    }
}
