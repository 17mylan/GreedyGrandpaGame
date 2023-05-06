using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[Description("Script à mettre sur le gameManager, il gère le système de wave ainsi que le spawn des ennemis")]
public class SpawnerEnnemi : MonoBehaviour
{
    [Tooltip("Liste des spawners de l'arène")]
    public List<GameObject> spawners = new List<GameObject>();
    
    [Space(10)]
    
    [Tooltip("Liste des ennemis actuellement spawns")]
    public List<GameObject> ennemis = new List<GameObject>();
    
    [Space(10)]
    
    [Tooltip("Prefab de l'ennemi")]
    public GameObject ennemi;

    [Space(10)]
    
    [Tooltip("Nombre d'ennemis à faire spawn par vague")]
    public int nbEnnemisParVague;
    
    [Space(10)]
    
    [Tooltip("Nombre de vagues par salle")]
    public int nbVagues;
    
    [Space(10)]
    
    [Tooltip("Numéro de la vague actuelle")]
    public int vagueActuelle = 1;
    
    [Space(10)]
    
    [Tooltip("Temps entre chaque vague")]
    public float tempsEntreVagues;
    
    [Space(10)]
    
    [Tooltip("Temps entre chaque spawn d'ennemi")]
    public float tempsEntreSpawns;
    
    
    // Variables internes
    private bool waveIsRunning;
    private float timerSpawnEnemy;
    public int enemySpawned;
    private bool actionDoned;
    public int kills;
    public TextMeshProUGUI currentWaveText;
    public AudioSource audioSource;
    public AudioClip newWaveClip;
    private void Start()
    {
        vagueActuelle = 1;
        currentWaveText.text = "Current Wave: " + vagueActuelle;
        timerSpawnEnemy = tempsEntreVagues;
        waveIsRunning = true;
        PlayerPrefs.SetInt("Wave", 1);
    }

    void Update()
    {
        
        if (waveIsRunning)
        {
            if (enemySpawned < nbEnnemisParVague && enemySpawned != nbEnnemisParVague)
            {
                
                waveIsRunning = true;
                actionDoned = false;
                if(Time.time >= timerSpawnEnemy + tempsEntreSpawns)
                {
                    SpawnEnemy();
                    //Debug.Log("Je spawn des ennemis");
                    timerSpawnEnemy = Time.time;
                }
                
            }
            else if( enemySpawned >= nbEnnemisParVague && ennemis.Count == 0)
            {
                waveIsRunning = false;
            }
        }
        
        if (!waveIsRunning)
        {
            if(vagueActuelle <= nbVagues)
            {
                if (!actionDoned)
                {
                    Invoke("NextWave", tempsEntreVagues);
                    waveIsRunning = true;
                    actionDoned = true;
                    return;
                }
            }

            // Apres avoir gagné
            else
            {
                SceneManager.LoadScene("RealMenu");
            }
        }
    }
    
    public void SpawnEnemy()
    {
        enemySpawned++;
        int randomSpawner = Random.Range(0, spawners.Count);
        GameObject newEnnemi = Instantiate(ennemi, spawners[randomSpawner].transform.position, Quaternion.identity);
        ennemis.Add(newEnnemi);
        
    }
    /// <summary>
    /// Fonction permettant de lancer une nouvelle vague, de reset le nombre d'enneis spawn et d'augmenter le nombre d'ennemis par vague
    /// </summary>
    public void NextWave()
    {
        enemySpawned = 0;
        UIManager.instance.ResetKills();
        UIManager.instance.NewWaveAnnouncement();
        vagueActuelle++;
        audioSource.PlayOneShot(newWaveClip);
        currentWaveText.text = "Current Wave: " + vagueActuelle;
        PlayerPrefs.SetInt("Wave", vagueActuelle);
        nbEnnemisParVague = 30 + 15 * vagueActuelle;
        waveIsRunning = true;
        //Debug.Log("Je vais lancer une nouvelle vague");
        actionDoned = true;

    }

}
