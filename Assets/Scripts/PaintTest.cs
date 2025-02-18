using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PaintTest : MonoBehaviour
{
    public AudioSource audioSource;
    private InputDevice rightController;

    void Start()
    {
        // Detect right-hand controller (You can adjust this for left hand if needed)
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void Update()
    {
        if (rightController.isValid)
        {
            bool buttonPressed;

            // Change "CommonUsages.primaryButton" to "triggerButton" if using the trigger instead
            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
