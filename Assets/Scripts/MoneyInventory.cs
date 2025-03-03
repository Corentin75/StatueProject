using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyInventory : MonoBehaviour
{
    public static MoneyInventory Instance; // Singleton 
    public float money = 1000; 
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

    public bool SpendMoney(float amount)
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

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: " + money;
    }
}
