using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyingScript : MonoBehaviour
{
    public int BasePrice;
    public int AugmentPrice;
    public GameObject Player;
    public int WeaponLevel;
    private int Price;
    private int PlayerMoney;
    public string WeaponLevelName;
    public TextMeshProUGUI PriceText;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        WeaponLevel = (int)Player.GetComponentInChildren<WeaponManager>().GetType().GetField(WeaponLevelName).GetValue(Player.GetComponentInChildren<WeaponManager>());
        Price = BasePrice + (AugmentPrice * WeaponLevel);
        PriceText.text = Price.ToString();
    }

    private void UpdatePrice()
    {
        Price = BasePrice + (AugmentPrice * WeaponLevel);
        PriceText.text = Price.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMoney = Player.GetComponent<PlayerMovement>().money;
            if (PlayerMoney >= Price)
            {
                Player.GetComponent<PlayerMovement>().money -= Price;
                WeaponLevel++;
                Player.GetComponentInChildren<WeaponManager>().GetType().GetField(WeaponLevelName).SetValue(Player.GetComponentInChildren<WeaponManager>(), WeaponLevel);
                UpdatePrice();
            }
        }
    }
}
