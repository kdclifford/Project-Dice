using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    ParticleSystem.Particle[] currentParticles;
    public int manaAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void OnParticleCollision(GameObject other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer("Mana"))
        //{

        currentParticles = new ParticleSystem.Particle[10];
           int numParticles =  other.GetComponent<ParticleSystem>().GetParticles( currentParticles);    

            for(int i = 0; i < numParticles; i++)
            {
                float dist = Vector3.Distance(transform.position, currentParticles[i].position);
            Debug.Log(dist);
                if(dist < 2)
                {
                    currentParticles[i].remainingLifetime = 0.0f;
                    manaAmount++;
                }
            }

            other.GetComponent<ParticleSystem>().SetParticles(currentParticles);
        //}


    }

}
