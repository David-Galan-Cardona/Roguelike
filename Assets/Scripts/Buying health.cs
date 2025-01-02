using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buyinghealth : MonoBehaviour
{
    public int Price = 3;
    public PlayerMovement Player;
    private int PlayerMoney;
    public TextMeshProUGUI PriceText;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        PriceText.text = Price.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMoney = Player.money;
            if (PlayerMoney >= Price)
            {
                Player.money -= Price;
                Player.HP += 1;
            }
        }
    }
}
