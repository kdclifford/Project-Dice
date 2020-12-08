using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRandomizer : MonoBehaviour
{
    ParticleSystem.MainModule ps;
    ParticleSystem.EmissionModule psem;
    // Start is called before the first frame update
    void Start()
    {
        int count = Random.Range(4, 10);
        ps = GetComponent<ParticleSystem>().main;
        ps.maxParticles = count;
        psem.burstCount = count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
