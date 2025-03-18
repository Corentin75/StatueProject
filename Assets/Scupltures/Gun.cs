using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Gun : MonoBehaviour
{
    public GameObject gun;
    public LayerMask layerMask;
    public float brushSize = 0.1f;
    public Texture2D paintTexture; // Texture de pinceau
    public Material paintMaterial; // Matériau de peinture
    public Color paintColor = Color.red; // Couleur de peinture
    public SprayController particle;

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(gun.transform.position, gun.transform.forward);
        StartCoroutine(particle.PlayParticle());

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
}