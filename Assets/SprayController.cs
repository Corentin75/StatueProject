using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public Material newMaterial; // Nouveau matériau à appliquer

    private void Start()
    {
        sprayParticles.Stop(); // Stop les particules au début
        ChangeParticleMaterial(); // Applique le nouveau matériau
    }

    public IEnumerator PlayParticle()
    {
        sprayParticles.Play();
        Debug.Log("particule");
        yield return new WaitForSeconds(1.5f);
        sprayParticles.Stop();
    }

    private void ChangeParticleMaterial()
    {
        // Récupère le Renderer du système de particules
        ParticleSystemRenderer renderer = sprayParticles.GetComponent<ParticleSystemRenderer>();
        if (renderer != null && newMaterial != null)
        {
            renderer.material = newMaterial; // Applique le matériau
            Debug.Log("Matériau des particules changé !");
        }
        else
        {
            Debug.LogWarning("Renderer ou matériel non assigné !");
        }
    }
}
