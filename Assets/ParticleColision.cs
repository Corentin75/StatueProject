using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public GameObject tagPrefab; // Préfab pour le tag

    private void OnParticleCollision(GameObject other)
    {
        // Détecter la collision avec une particule
        Vector3 collisionPosition = other.transform.position;

        // Instancier le tag à la position de la collision
        Instantiate(tagPrefab, collisionPosition, Quaternion.identity);
    }
}