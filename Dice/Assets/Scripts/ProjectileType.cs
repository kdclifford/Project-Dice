﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{ 
    public int spellIndex;
    [HideInInspector]
   public SpellList spellList;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SpellList.instance.spells[spellIndex].UILogo;
        transform.GetChild(0).gameObject.GetComponent<Light>().color = SpellList.instance.spells[spellIndex].castingColour;
    }

    public void LoadUI()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spellList.spells[spellIndex].UILogo;
        //transform.GetChild(0).gameObject.GetComponent<Light>().color = SpellList.instance.spells[spellIndex].castingColour;
    }

    public void setSpell()
    {
        spellList = GameObject.FindGameObjectWithTag("SpellManager").GetComponent<SpellList>();
    }

   

}


public enum Projectile
{
    Bubble,
    FireBall,
    Electric,
}
