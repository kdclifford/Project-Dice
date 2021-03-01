using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{    
    public float maxHealth = 0;
    //[HideInInspector]
    //[SerializeField]
    private float currentHealth = 1;

    public float maxShields;
    public float currentShields;
    GameObject agentRoot;

    private LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        currentHealth = maxHealth;
        currentShields = 0;
        agentRoot = transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 )
        {
            //Load Another Scene 
            if(gameObject.tag == "Player")
            {
                levelManager.LoadLevel("DeathRoom");
            }
            
            
            //currentHealth = 1;
            //Reset The player Data on death
            DestroyComponents();
        }
    }


    public void AddHealth()
    {
        if (currentHealth < maxHealth)
        {
            if(currentHealth + 25 > 100)
            {
                currentHealth = 100;
            }
            else
            {
            currentHealth += 25;
            }

        }
    }

    public void RemoveHealth()
    {        
        if(currentShields > 0)
        {
            currentShields--;
        }
        else
        {
            currentHealth--;   
        }
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
