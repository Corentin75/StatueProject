using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDropdown : MonoBehaviour
{
    public GameObject arrow;
    public GameObject choice;
    public bool isToggled;
    public float rotationDuration = 0.2f;

    void Start()
    {
        isToggled = false;
    }

    public void ToggleDropdown()
    {
        float targetRotation = isToggled ? -90f : 0f;
        StartCoroutine(RotateArrow(targetRotation));
        choice.SetActive(isToggled);
        isToggled = !isToggled;
    }

    IEnumerator RotateArrow(float targetZ)
    {
        Quaternion startRotation = arrow.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetZ);
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            arrow.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / rotationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        arrow.transform.rotation = targetRotation;
    }
}
