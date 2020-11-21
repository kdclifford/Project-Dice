using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{    
    public float maxHealth = 0;
    //[HideInInspector]
    [SerializeField]
    private float currentHealth;

    public float maxShields;
    private float currentShields;
    GameObject agentRoot;    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentShields = 0;
        agentRoot = transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 )
        {
            DestroyComponents();
        }
    }


    public void AddHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
        }
    }

    public void RemoveHealth()
    {        
            currentHealth--;          
    }

    public int GetHealth()
    {
        return (int)currentHealth;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
    }

    public void AddShield()
    {
        if (currentShields < maxShields)
        {
            currentShields++;           
        }
    }

    public int GetShield()
    {
        return (int)currentShields;
    }

    public void SetShield(int shield)
    {
        currentShields = shield;
    }


    private void DestroyComponents()
    {
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());
    }

}
