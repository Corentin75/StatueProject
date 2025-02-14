using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInput : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction primaryButtonAction;
    private Coroutine shootingCoroutine;

    private void Awake()
    {
        var actionMap = inputActions.FindActionMap("VR");
        primaryButtonAction = actionMap.FindAction("Primary");

        primaryButtonAction.performed += OnPrimaryButtonPressed;
        primaryButtonAction.canceled += OnPrimaryButtonReleased;
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
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
    }

    private void OnPrimaryButtonReleased(InputAction.CallbackContext context)
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            GetComponent<Gun>().Shoot();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
