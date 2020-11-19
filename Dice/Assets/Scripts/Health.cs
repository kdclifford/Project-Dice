using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{    
    public float maxHealth = 0;
    //[HideInInspector]
    public float currentHealth;

    GameObject agentRoot;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

    //public void addShield()
    //{
    //    if (currentShield < maxShield)
    //    {
    //        currentShield++;
    //        ShieldUIIcons[currentShield].gameObject.SetActive(true);
    //    }
    //}



    private void DestroyComponents()
    {
        Destroy(agentRoot.GetComponent<Rigidbody>());
        Destroy(agentRoot.GetComponent<Collider>());
    }

}
