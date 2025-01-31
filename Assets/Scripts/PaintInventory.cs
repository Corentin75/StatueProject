using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PaintInventory : MonoBehaviour
{
    private int maxCapacity;
    private int currentCapacity;

    private float scaleDiminution;

    public float duration = 10f;

    public GameObject paintTank;

    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = 10;
        currentCapacity = maxCapacity;

        scaleDiminution = 1 / maxCapacity;

        StartCoroutine(ReduceScaleOverTime());
    }

    // Update is called once per frame
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

            yield return null; // Attendre la frame suivante
        }

        // S'assurer que l'objet atteint bien la taille minimale
        paintTank.transform.localScale = targetScale;
    }

    //public void Vidage()
    //{
    //}
}
