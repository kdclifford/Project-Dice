using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkingExpolosion : SpellBase
{
    // Start is called before the first frame update
    public override void CastSpell(Vector3 posistion, Quaternion playerRot, string tag = "Equipped")
    {

        //Summon A Pumkin 

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadious, targetMask);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/Electric", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/LightningUI");
    }
}
