using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkParticleSpawner : MonoBehaviour
{
    private ParticleSystem damageParticles; 
    // Start is called before the first frame update
    void Start()
    {
        damageParticles = GetComponent<ParticleSystem>();   
    }

    
    public void SpurtInk()
    {
        damageParticles.Play();
    }
}
