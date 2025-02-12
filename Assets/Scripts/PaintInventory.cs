using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PaintInventory : MonoBehaviour
{
    public Material[] materialsContainer;
    private Material paintMat;

    private float maxCapacity;
    private float currentCapacity;

    private float currentNum;

    private float scaleDiminution;

    public float duration = 10f;

    private float maxScale;
    private float minScale;
    private float currentScale;
    private float scaleFactor;

    public GameObject paintTank;
    public GameObject[] Weapons;

    // Start is called before the first frame update
    void Start()
    {
        paintMat = GetComponent<Material>();

        currentNum = 0;

        maxCapacity = 10f;
        currentCapacity = maxCapacity;

        maxScale = 1f;
        minScale = 0.01f;
        currentScale = maxScale;

        scaleDiminution = (1f / maxCapacity) - 0.1f;

        Debug.Log(scaleDiminution);

        StartCoroutine(ReduceScaleOverTime());
    }

    public void AddPaintStock(int quantity)
    {
        currentCapacity += quantity;

        if(currentCapacity > maxCapacity)
        {
            currentCapacity = maxCapacity;
        }

        scaleFactor = (float)currentCapacity / maxCapacity;

        StartCoroutine(ReduceScaleOverTime2(scaleFactor)); 
    }

    public void RemovePaintStock(int quantity)
    {
        if(quantity > currentCapacity)
        {
            Debug.Log("Usage impossible");

            return;
        }

        currentCapacity -= quantity;

        if (currentCapacity < 0)
        {
            currentCapacity = minScale;
        }

        scaleFactor = (float)currentCapacity / maxCapacity;

        StartCoroutine(ReduceScaleOverTime2(scaleFactor)); 
    }

    IEnumerator ReduceScaleOverTime()
    {
        float elapsedTime = 0f;
        Vector3 initialScale = paintTank.transform.localScale;
        Vector3 targetScale = new Vector3(1, scaleDiminution, 1); // Objectif

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Interpolation entre la taille initiale et la taille minimale
            paintTank.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

            yield return null; 
        }

        paintTank.transform.localScale = targetScale;
    }

    IEnumerator ReduceScaleOverTime2(float scalefactor)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = paintTank.transform.localScale;
        Vector3 targetScale = new Vector3(1, scalefactor, 1); // Objectif

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Interpolation entre la taille initiale et la taille minimale
            paintTank.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

            yield return null; 
        }

        paintTank.transform.localScale = targetScale;
    }

    public void ChangerNum()
    {
        if(currentNum == 2)
        {
            currentNum = 0;
        }
        else
        {
            currentNum += 1;
        }
    }

    public void ChangeMaterial(int numéro)
    {
        switch(numéro)
        {
            case 0:
                Debug.Log("numéro : " + numéro + " , arme : " + Weapons[0]);
                break;
            case 1:
                Debug.Log("numéro : " + numéro + " , arme : " + Weapons[1]);
                break;
            case 2:
                Debug.Log("numéro : " + numéro + " , arme : " + Weapons[2]);
                break;

        }
    }

}


