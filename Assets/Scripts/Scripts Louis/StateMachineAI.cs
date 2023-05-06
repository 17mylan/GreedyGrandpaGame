using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;


public class StateMachineAI : MonoBehaviour
{
    //Variables d'A*
    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;
    
    
    [Space(10)]
    
    //Variables relatives à la vitesse de l'IA
    [Tooltip("Vitesse maximale de l'IA")]
    public float maxSpeed;
    [Tooltip("Vitesse d'accélération de l'IA")]
    public float acceleration;
    
    [Space(10)]
    
    //Statistiques de l'IA (dégâts d'attaque, points de vie, etc...)
    [Tooltip("Dégâts d'attaque de l'IA")]
    public float attackDamage;
    [Tooltip("Points de vie de l'IA")]
    public float healthPoints;
    [Tooltip("Points de vie maximum de l'IA")]
    public float maxHealthPoints;
    [Tooltip("Temps entre chaque attaque de l'IA")]
    public float attackSpeed;

    [Space(10)]

    //Variables internes à l'IA
    public GameObject iaBody;
    private SpawnerEnnemi spawnerEnnemi;
    private GameObject player;
    private float timerAttack;
    private float timerSwitchAnimation;
    private float cooldownSwitchAnimation = 1f;
    
    //Variables game feel
    
    [Tooltip("SFX cri zombie")]
    public AudioSource source;
    public AudioClip zombieScream;
    public Animator animator;

    void Start()
    {
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = maxSpeed;
        aiPath.maxAcceleration = acceleration;
        spawnerEnnemi = GameObject.Find("GameManager").GetComponent<SpawnerEnnemi>();
        iaBody = transform.GetChild(0).gameObject;
        aiDestinationSetter.target = GameObject.Find("Player Armature").transform;
        player = aiDestinationSetter.target.gameObject;
        
        
    }


    void Update()
    {
        
        
        if (aiPath.reachedEndOfPath)
        {
            if (animator != null)
                animator.SetBool("isAttacking", true);
            
            //source.PlayOneShot(zombieScream);
            if(Time.time >= timerAttack + attackSpeed)
            {
                
                player.GetComponent<PlayerShooting>().TakePlayerDamage(attackDamage);
                //animator.SetBool("isAttacking", true);
                timerAttack = Time.time + attackSpeed;
            }
        }
        else
        {
            if(Time.time >= timerSwitchAnimation + cooldownSwitchAnimation)
            {
                if(animator != null)
                    animator.SetBool("isAttacking", false);
                timerSwitchAnimation = Time.time + cooldownSwitchAnimation;
            }
            
            //Mettre variable pour anim marche et attaque
        }
    }
    
    
    //Fonction pour infliger des dégâts à l'IA
    
    public void TakeDamage(float damage)
    {
        if(healthPoints == 0) return;
        
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Die();
        }
    }
    
    //Fonction pour tuer l'IA
    
    void Die()
    {
        spawnerEnnemi.ennemis.Remove(gameObject);
        spawnerEnnemi.kills += 1;
        GameManager.instance.money += 50;
        UIManager.instance.UpdateWaveCounter();
        Destroy(iaBody);
        Destroy(gameObject, 0.1f);
        
    }
    
}
