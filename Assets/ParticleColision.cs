using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public GameObject tagPrefab; // Pr�fab pour le tag

    private void OnParticleCollision(GameObject other)
    {
        // D�tecter la collision avec une particule
        Vector3 collisionPosition = other.transform.position;

        // Instancier le tag � la position de la collision
        Instantiate(tagPrefab, collisionPosition, Quaternion.identity);
    }
}