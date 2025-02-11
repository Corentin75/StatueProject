using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject canvaMainShop, canvaWeapons, canvaPaints;

    [System.Serializable]
    public class ShopItem
    {
        public Button button; 
        public int price; 
    }

    public ShopItem[] shopItems; 

    private void Start()
    {
        foreach (ShopItem item in shopItems)
        {
            if (item.button != null)
            {
                item.button.onClick.AddListener(() => BuyItem(item.price));
            }
        }
    }

    private void BuyItem(int price)
    {
        if (MoneyInventory.Instance.SpendMoney(price))
        {
            Debug.Log("Achat réussi pour " + price + " !");
        }
        else
        {
            Debug.Log("Pas assez d'argent !");
        }
    }

    public void OpenWeapons()
    {
        canvaMainShop.SetActive(false);
        canvaWeapons.SetActive(true);
    }

    public void CloseWeapons()
    {
        canvaMainShop.SetActive(true);
        canvaWeapons.SetActive(false);
    }

    public void OpenPaints()
    {
        canvaMainShop.SetActive(false);
        canvaPaints.SetActive(true);
    }

    public void ClosePaints()
    {
        canvaMainShop.SetActive(true);
        canvaPaints.SetActive(false);
    }
}
