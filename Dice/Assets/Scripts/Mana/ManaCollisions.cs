using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollisions : MonoBehaviour
{
    ParticleSystem.Particle[] currentParticles;
    ManaManager manaManager;

    // Start is called before the first frame update
    void Start()
    {
        manaManager = ManaManager.instance;
    }



    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 15)
        {
            currentParticles = new ParticleSystem.Particle[20];
            int numParticles = other.GetComponent<ParticleSystem>().GetParticles(currentParticles);

            for (int i = 0; i < numParticles; i++)
            {
                float dist = Vector3.Distance(transform.position, currentParticles[i].position);
                // Debug.Log(dist);
                if (dist < 3)
                {
                    currentParticles[i].remainingLifetime = 0.0f;
                    // GiveMana(other.tag);
                    Mana.instance.AddMana(1);
                }
            }

            other.GetComponent<ParticleSystem>().SetParticles(currentParticles);
        }
    }


    void GiveMana(string tag)
    {
        switch (tag)
        {
            case "Fire":
                manaManager.Fire = manaManager.Fire + 1;
                break;
            case "Water":
                manaManager.Water = manaManager.Water + 1;
                break;
            case "Rock":
                manaManager.Rock = manaManager.Rock + 1;
                break;
            case "Electricity":
                manaManager.Electricity = manaManager.Electricity + 1;
                break;
            case "Wind":
                manaManager.Wind = manaManager.Wind + 1;
                break;
        }
    }


}
