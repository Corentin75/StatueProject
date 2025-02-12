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
    public Texture2D paintTexture;
    public Material paintMaterial;
    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(gun.transform.position, gun.transform.forward);

        if (Physics.Raycast(ray, out hit) && layerMask.Contains(hit.collider.gameObject.layer))
        {
            var sculpture = hit.collider.gameObject.GetComponent<Sculpture>();
            if (sculpture != null)
            {
                Vector2 uv = hit.textureCoord;
                sculpture.Paint(uv,brushSize);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gun.transform.position, gun.transform.forward);
    }
}