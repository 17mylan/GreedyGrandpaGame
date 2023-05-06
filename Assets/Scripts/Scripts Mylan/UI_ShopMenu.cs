using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ShopMenu : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerShooting playerShooting;
    public TextMeshProUGUI dollarText;
    public TextMeshProUGUI grenadeText;
    public TextMeshProUGUI heathText;
    public bool isHeathAlreadyUpgrade = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerShooting = FindObjectOfType<PlayerShooting>();
    }
    public void Update()
    {
        dollarText.text = gameManager.money.ToString();
    }
    public void BuyHealth()
    {
        if(gameManager.money >= 5000)
        {
            gameManager.money -= 5000;
            GameManager.instance.UpdateMoneyHUD();
            var healthDiff = playerShooting.playerMaxHealth - playerShooting.playerHealth;
            if (healthDiff > 25f)
                playerShooting.playerHealth += 25f;
            else
                playerShooting.playerHealth += healthDiff;
        }
    }
    public void BuyGrenade()
    {
        if(gameManager.money >= 2000)
        {
            gameManager.money -= 2000;
            GameManager.instance.UpdateMoneyHUD();
            playerShooting.grenadeNumber++;
            grenadeText.text = playerShooting.grenadeNumber.ToString();
        }
    }
    public void BuyHeath()
    {
        if(gameManager.money >= 20000 && !isHeathAlreadyUpgrade)
        {
            gameManager.money -= 20000;
            GameManager.instance.UpdateMoneyHUD();
            playerShooting.MaxHeat = 200f;
            isHeathAlreadyUpgrade = true;
            heathText.text = "Level 2 / 2".ToString();
        }
    }
}