using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    none,
    heat,
    heal,
    grenade
}

public class Shop : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject shopUI;
    public PlayerShooting playerShooting;

    public int heatUpgradePrice;
    public int lifePrice;
    public int grenadePrice;

    private ItemType selectedType = ItemType.none;
    private TextMeshProUGUI itemName;
    
    void Start()
    {
        gameManager = GameManager.instance;
        UIManager.instance.UpdateDollarPanel();
        //if (shopUI.transform.GetChild(3).TryGetComponent(out TextMeshProUGUI tmp)) itemName = tmp;
    }


    private void Buy(int value)
    {
        if (gameManager.money >= value)
        {
            gameManager.money -= value;
            UIManager.instance.UpdateDollarPanel();
        }
        else
        {
            Debug.Log("Pas assez d'argent");
            //Mettre feedback manque argent
        }
    }
    
    public void Quit()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SelectType(int type)
    {
        selectedType = (ItemType) type;
        UIManager.instance.UpdateSlotSelected(type-1);
        UIManager.instance.ChangeBuyState(true);
        switch (selectedType)
        {
            case ItemType.none:
                itemName.text = "PLEASE CHOOSE AN ITEM";
                break;
            case ItemType.heat:
                itemName.text = "Overheating Upgrade";
                break;
            case ItemType.heal:
                itemName.text = "Heal";
                break;
            case ItemType.grenade:
                itemName.text = "Grenade";
                break;
        }
    }
    
    public void CheckType()
    {
        switch (selectedType)
        {
            case ItemType.heat:
                BuyHeatUpgrade();
                break;
            case ItemType.heal:
                BuyLife();
                break;
            case ItemType.grenade:
                BuyGrenade();
                break;
            case ItemType.none:
                break;
        }
    }
    
    public void BuyHeatUpgrade()
    {
        Buy(heatUpgradePrice);
        playerShooting.MaxHeat = 200f;
    }
    
    public void BuyLife()
    {
        Buy(lifePrice);
        var healthDiff = playerShooting.playerMaxHealth - playerShooting.playerHealth;
        if (healthDiff > 25f)
        {
            playerShooting.playerHealth += 25f;
        }
        else
        {
            playerShooting.playerHealth += healthDiff;
        }
    }
    
    public void BuyGrenade()
    {
        Buy(grenadePrice);
        playerShooting.grenadeNumber++;
    }

    public void Return()
    {
        Quit();
        playerShooting.leftMarchandMenu();
    }
}
