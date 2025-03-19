using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public GameObject gun;
    private Interactable interactable;
    public LayerMask layerMask;
    public float brushSize = 0.1f;
    public Texture2D paintTexture; // Texture de pinceau
    public Color paintColor = Color.red; // Couleur de peinture
    public SprayController particle;
    public SteamVR_Action_Boolean actionFire = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Gun", "FireGun");
    private Coroutine shootingCoroutine;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }
    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(gun.transform.position, gun.transform.forward);
        StartCoroutine(particle.PlayParticle());
        Debug.Log("Shoot");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Sculpture sculpture = hit.collider.gameObject.GetComponent<Sculpture>();
            if (sculpture != null)
            {
                sculpture.Paint(hit.point, brushSize, paintColor); // Peinture sur les vertices
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gun.transform.position, gun.transform.forward);
    }

    private void Update()
    {
        bool isFiring = false;
        if (interactable.attachedToHand)
        {
            Debug.Log(SteamVR_Input.actionsBoolean);
            SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
            isFiring = actionFire.state;
        }

        if (shootingCoroutine == null && isFiring)
        {
            Debug.Log("Starting shooting coroutine");
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }

        if (shootingCoroutine != null && !isFiring)
        {
            Debug.Log("Stopping shooting coroutine");
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }


    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(0.01f);
        }
    }
}