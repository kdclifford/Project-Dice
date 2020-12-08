using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRandomizer : MonoBehaviour
{
    ParticleSystem.MainModule ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>().main;
        ps.maxParticles = Random.Range(4, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
