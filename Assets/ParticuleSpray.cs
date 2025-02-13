using UnityEngine;
using UnityEngine.XR;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public XRNode controllerNode; // Choisir LeftHand ou RightHand

    private InputDevice controller;

    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
        sprayParticles.Stop();
    }

    void Update()
    {
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
        {
            Debug.Log("Trigger pressed: " + triggerPressed);
            if (triggerPressed)
            {
                if (!sprayParticles.isPlaying)
                {
                    sprayParticles.Play();
                }
            }
            else
            {
                if (sprayParticles.isPlaying)
                {
                    sprayParticles.Stop();
                }
            }
        }
    }
}