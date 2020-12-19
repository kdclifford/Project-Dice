using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkingExpolosion : SpellBase
{
    // Start is called before the first frame update
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        //Summon A Pumkin 

        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.PumkingExpolosion);
        //Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadious, targetMask);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load("Spells/PumkinSpell", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/LightningUI");
        SpellName = "PumpKing";
    }
}
