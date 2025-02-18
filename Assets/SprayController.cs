using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public void Start()
    {
        sprayParticles.Stop();
    }

    public IEnumerator PlayParticle()
    {
        sprayParticles.Play();
        Debug.Log("particule");
        yield return new WaitForSeconds(1.5f);
        sprayParticles.Stop();
    }
}