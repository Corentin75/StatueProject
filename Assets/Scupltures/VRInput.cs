using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInput : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction primaryButtonAction;

    private void Awake()
    {
        var actionMap = inputActions.FindActionMap("VR");
        primaryButtonAction = actionMap.FindAction("Primary");

        primaryButtonAction.performed += OnPrimaryButtonPressed;
    }

    private void OnEnable()
    {
        primaryButtonAction.Enable();
    }

    private void OnDisable()
    {
        primaryButtonAction.Disable();
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        GetComponent<Gun>().Shoot();

    }
}
