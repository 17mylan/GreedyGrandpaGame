using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables

    public static UIManager instance;

    [Header("UI References")]
    [SerializeField] public UI_Overheating UIOverheating;
    [SerializeField] public UI_PauseMenu UIPauseMenu;
    [SerializeField] public UI_WaveCounter UIWaveCounter;
    [SerializeField] public UI_HealthBar UIHealthBar;

    [Header("Others References")] 
    [SerializeField] public SpawnerEnnemi waveManager;
    [SerializeField] public PlayerShooting player;
    [SerializeField] public GameObject HUD;
    [SerializeField] public GameObject PauseMenu;
    [SerializeField] public GameObject ShopSlots;
    [SerializeField] public Button ShopButton;
    [SerializeField] public GameObject ShopDollar;

    [Header("Component References")]
    [SerializeField] public GameObject warningBarGO;

    public TextMeshProUGUI playerMoneyText;
    public TextMeshProUGUI playerGrenadeText;
    private PlayerShooting playerShooting;
    [Header("Prefabs")] [SerializeField] public GameObject WaveAnnouncementPrefab;
    private SpawnerEnnemi spawnerEnnemi;
    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    public void UpdateWaveCounter()
    {
        var mobsInWave = waveManager.nbEnnemisParVague;
        var mobsAlive = waveManager.enemySpawned;

        //print("mobsAlive :" + mobsAlive + "/nMobsInWave : " + mobsInWave);
        UIWaveCounter.ChangeText(waveManager.kills, mobsInWave);
        
    }

    public void ResetKills()
    {
        waveManager.kills = 0;
    }

    public void NewWaveAnnouncement()
    {
        WaveAnnouncementPrefab.SetActive(true);
        if (WaveAnnouncementPrefab.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI tmp))
        {
            tmp.text = $"Wave {waveManager.vagueActuelle+1}";
        }
        
    }

    public void Update()
    {
        var currentHP = player.playerHealth;
        var maxHP = player.playerMaxHealth;
        UIHealthBar.ChangeValueHealthBar(currentHP, maxHP);
    }

    public void UpdateSlotSelected(int index)
    {
        if (ShopSlots.transform.GetChild(index).TryGetComponent(out Button btn))
        {
            btn.Select();
            //print("Selected");
        }
    }

    public void ChangeBuyState(bool value)
    {
        ShopButton.interactable = value;
    }

    public void UpdateDollarPanel()
    {
        TextMeshProUGUI dollarText = null;
        if(ShopDollar.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI TMP))  dollarText = TMP;

        //if (dollarText != null) dollarText.text = GameManager.instance.money.ToString("000000");
    }
    public void UpdateDollarHUD()
    {
        playerMoneyText.text = GameManager.instance.money.ToString();
    }
    
    
}
