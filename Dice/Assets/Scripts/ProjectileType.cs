using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{ 
    public int spellIndex;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = SpellList.instance.spells[spellIndex].UILogo;
        transform.GetChild(0).gameObject.GetComponent<Light>().color = SpellList.instance.spells[spellIndex].castingColour;
    }



}


public enum Projectile
{
    Bubble,
    FireBall,
    Electric,
}
