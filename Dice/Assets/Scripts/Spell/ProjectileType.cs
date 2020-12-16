using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{ 
    public ESpellEnum spellIndex;
    [HideInInspector]
   public SpellList spellList;
    public bool randomSpell = true;

    private void Start()
    {
        if (randomSpell)
        {
            int randSpell = Random.Range(0, SpellList.instance.spells.Count);
            spellIndex = (ESpellEnum)randSpell;  
        } 
        gameObject.GetComponent<SpriteRenderer>().sprite = SpellList.instance.spells[(int)spellIndex].UILogo;
            transform.GetChild(0).gameObject.GetComponent<Light>().color = SpellList.instance.spells[(int)spellIndex].castingColour;
    }

    public void LoadUI()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spellList.spells[(int)spellIndex].UILogo;
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
