using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{ 
    public ESpellEnum spellIndex;
    public bool randomSpell = true;
    public int tierOfSpell = 0;

    public TextMesh NameText;
    public TextMesh TypeText;
    public TextMesh ElementText;
    public TextMesh ChargeText;

    private void Start()
    {
        while (randomSpell)
        {
            int randSpell = Random.Range(0, (int)ESpellEnum.Size - 2);
            if(SpellList.instance.spells[randSpell].unlockTier == tierOfSpell)
            {
                spellIndex = (ESpellEnum)randSpell;
                randomSpell = false;
            }
        } 

        gameObject.GetComponent<SpriteRenderer>().sprite = SpellList.instance.spells[(int)spellIndex].UILogo;
        transform.GetChild(0).gameObject.GetComponent<Light>().color = SpellList.instance.spells[(int)spellIndex].castingColour;

        TypeText.text = SpellList.instance.spells[(int)spellIndex].spellType.ToString();
        ElementText.text = SpellList.instance.spells[(int)spellIndex].element.ToString();
        ElementText.text = SpellList.instance.spells[(int)spellIndex].element.ToString();
        NameText.text = SpellList.instance.spells[(int)spellIndex].SpellName.ToString();
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void LoadUI()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SpellList.instance.spells[(int)spellIndex].UILogo;
        //transform.GetChild(0).gameObject.GetComponent<Light>().color = SpellList.instance.spells[spellIndex].castingColour;
    }
}


public enum Projectile
{
    Bubble,
    FireBall,
    Electric,
}
