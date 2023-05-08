using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private StateMachineAI stateMachineAI;
    public float bulletDamage;
    public ParticleSystem collisionParticles, bloodParticles;
    private PlayerShooting playerShooting;
    private GameManager gameManager;
    private int levelToAdd;
    public void Start()
    {
        gameManager = GameManager.instance;
        playerShooting = FindObjectOfType<PlayerShooting>();
        transform.rotation = transform.rotation * Quaternion.Euler(90f, 90f, 90f);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //playerShooting.AddPlayerMoney(50);
            stateMachineAI = collision.gameObject.GetComponent<StateMachineAI>();
            stateMachineAI.TakeDamage(bulletDamage);
            Instantiate(bloodParticles, collision.contacts[0].point, Quaternion.identity);
            GameManager.instance.UpdateMoneyHUD();
            levelToAdd = PlayerPrefs.GetInt("Experience");
            PlayerPrefs.SetInt("Experience", levelToAdd + 2);
        }
        else if (collision.gameObject.CompareTag("Brique"))
        {
            Transform parent = collision.gameObject.transform.parent;
            Destroy(parent.gameObject);
        }
        else
        {
            Instantiate(collisionParticles, collision.contacts[0].point, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
