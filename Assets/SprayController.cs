using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public Material newMaterial; // Nouveau mat�riau � appliquer

    private void Start()
    {
        sprayParticles.Stop(); // Stop les particules au d�but
        ChangeParticleMaterial(); // Applique le nouveau mat�riau
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
        // R�cup�re le Renderer du syst�me de particules
        ParticleSystemRenderer renderer = sprayParticles.GetComponent<ParticleSystemRenderer>();
        if (renderer != null && newMaterial != null)
        {
            renderer.material = newMaterial; // Applique le mat�riau
            Debug.Log("Mat�riau des particules chang� !");
        }
        else
        {
            Debug.LogWarning("Renderer ou mat�riel non assign� !");
        }
    }
}
