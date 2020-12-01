using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase
{
    public GameObject SpellObject;
    public abstract void CastSpell(Vector3 posistion, Quaternion playerRot, string tag = "Equipped");
    public abstract void SetValues();
    public float durition;
    public EElementalyType element;
    public Color castingColour;
    public ESpellType spellType;
    public ESoundClipEnum castingSound;
    public Sprite UILogo;

    //Data Structure for UI Needs to be defineded
    //Maybe another Abstract Function called "GetUIData" in base class that can be called by children to feedback Data


    public void ProjectileFire(Vector3 posistion, Quaternion playerRot, string tag, float projectileSpeed)
    {
        //Spell Code would go in here then when controller has correct input Cast Spell
        GameObject bullet = MonoBehaviour.Instantiate(SpellObject, posistion, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
        bullet.tag = tag;
        bullet.GetComponent<ProjectileController>().setTimer(durition);
    }


}
