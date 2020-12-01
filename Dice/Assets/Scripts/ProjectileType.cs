using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{ 
   // public SpellBase spell = new FireBall();
    public Projectile projectileType;

    // Start is called before the first frame update
    void Start()
    {
       // GetComponent<SpriteRenderer>().sprite = projectileType.UILogo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public enum Projectile
{
    Bubble,
    FireBall,
    Electric,
}
