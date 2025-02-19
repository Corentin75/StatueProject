using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDropdown : MonoBehaviour
{
    public GameObject arrow;
    public GameObject choice;
    public bool isToggled;

    void Start()
    {
        isToggled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDropdown()
    {
        if (isToggled)
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            choice.SetActive(false);
        }
        else
        {
            arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
            choice.SetActive(true);
        }

        isToggled = !isToggled;
    }
}
