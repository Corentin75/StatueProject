using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInventory : MonoBehaviour
{
    public static MoneyInventory Instance; // Singleton 
    public int money = 1000; 
    public TextMeshProUGUI moneyText; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
            return true; 
        }
        return false; 
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: " + money;
    }
}
