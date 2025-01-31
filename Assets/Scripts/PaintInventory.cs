using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintInventory : MonoBehaviour
{
    private int maxCapacity;
    private int currentCapacity;

    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = 10;
        currentCapacity = maxCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentCapacity -= 1;
            Debug.Log("Peinture actuelle : " + currentCapacity);
        }
    }

    //public void Vidage()
    //{
    //}
}
