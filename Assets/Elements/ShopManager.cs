using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject canvaMainShop, canvaWeapons, canvaPaints;

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
