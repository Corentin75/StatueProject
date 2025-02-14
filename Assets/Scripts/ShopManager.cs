using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject LayerPrefab;

    public GameObject canvaMainShop, canvaWeapons, canvaPaints;

    public Transform bodyWeapons;
    public Transform bodypaintMat;

    public WeaponData[] weaponData;

    public PaintMatData[] paintMatData;

    private void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (WeaponData data in weaponData)
        {
            GameObject newWeapon = Instantiate(weaponPrefab, bodyWeapons);
            newWeapon.transform.localScale = Vector3.one;

            // Trouver l'Image du Weapon et changer la source (sprite)
            Image weaponImage = newWeapon.GetComponent<Image>();
            if (weaponImage != null)
            {
                weaponImage.sprite = data.weaponIcon;
            }

            // Met à jour le texte du nom 
            TextMeshProUGUI textName = newWeapon.transform.Find("TextName").GetComponent<TextMeshProUGUI>();
            if (textName != null)
            {
                textName.text = $"{data.weaponName}";
            }

            // Mettre à jour le texte d'achat
            Transform buy = newWeapon.transform.Find("Buy");
            if (buy == null)
            {
                Debug.LogError("'Buy' non trouvé sous " + newWeapon.name);
            }
            TextMeshProUGUI textBuy = buy.Find("BuyPrix").GetComponent<TextMeshProUGUI>();
            if (textBuy != null)
            {
                textBuy.text = $"Acheter ${data.weaponPrice}";
                Debug.Log("Prix affiché : " + textBuy.text);

                // Ajouter un bouton d'achat
                Button buyButton = buy.GetComponent<Button>();
                if (buyButton != null)
                {
                    // Associer l'action d'achat avec la méthode BuyItem en passant les données de l'arme
                    buyButton.onClick.AddListener(() => BuyItem(data));
                }
            }
            else
            {
                Debug.LogError(" 'TextBuy' non trouvé sous 'Buy' sur " + newWeapon.name);
            }
        }

        foreach (PaintMatData data in paintMatData)
        {
            GameObject newPaintMat = Instantiate(LayerPrefab, bodypaintMat);
            newPaintMat.transform.localScale = Vector3.one;

            // Trouver l'Image du Weapon et changer la source (sprite)
            Image weaponImage = newPaintMat.GetComponent<Image>();
            if (weaponImage != null)
            {
                weaponImage.sprite = data.paintMatIcon;
            }

            // Met à jour le texte du nom 
            TextMeshProUGUI textName = newPaintMat.transform.Find("TextName").GetComponent<TextMeshProUGUI>();
            if (textName != null)
            {
                textName.text = $"{data.paintMatName}";
            }

            // Mettre à jour le texte d'achat
            Transform buy = newPaintMat.transform.Find("Buy");
            if (buy == null)
            {
                Debug.LogError("'Buy' non trouvé sous " + newPaintMat.name);
            }
            Text textBuy = buy.Find("BuyPrix").GetComponent<Text>();
            if (textBuy != null)
            {
                textBuy.text = $"Acheter ${data.paintMatPrice}";
                Debug.Log("Prix affiché : " + textBuy.text);

                // Ajouter un bouton d'achat
                Button buyButton = buy.GetComponent<Button>();
                if (buyButton != null)
                {
                    // Associer l'action d'achat avec la méthode BuyItem en passant les données de l'arme
                    buyButton.onClick.AddListener(() => BuyItem(data));
                }
            }
            else
            {
                Debug.LogError(" 'TextBuy' non trouvé sous 'Buy' sur " + newPaintMat.name);
            }
        }
    }

    public void BuyItem(WeaponData weapon)
    {
        if (MoneyInventory.Instance.SpendMoney(weapon.weaponPrice)) 
        {
            Debug.Log("Achat réussi pour " + weapon.weaponName + " au prix de " + weapon.weaponPrice + " !");
        }
        else
        {
            Debug.Log("Pas assez d'argent pour acheter " + weapon.weaponName + " !");
        }
    }

    public void BuyItem(PaintMatData paintMat)
    {
        if (MoneyInventory.Instance.SpendMoney(paintMat.paintMatPrice))
        {
            Debug.Log("Achat réussi pour " + paintMat.paintMatName + " au prix de " + paintMat.paintMatPrice + " !");
        }
        else
        {
            Debug.Log("Pas assez d'argent pour acheter " + paintMat.paintMatPrice + " !");
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
