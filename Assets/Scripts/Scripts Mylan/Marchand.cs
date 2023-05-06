using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Marchand : MonoBehaviour
{
    private GameManager gameManager;
    public PlayerShooting playerShooting;
    public int HeatPrice, GrenadePrice, LifePrice;
    public TextMeshProUGUI currentPlayerMoney;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        currentPlayerMoney.text = gameManager.money.ToString();
        //print(gameManager.money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
