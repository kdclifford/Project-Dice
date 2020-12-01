using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, Quaternion playerRot)
    {
        //Spell Code would go in here then when controller has correct input Cast Spell
        GameObject bullet = Instantiate(SpellList.instance.spells[2], posistion, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
        GetComponent<ProjectileController>().setTimer(durition);
        //soundManager.Play(castingSound, bullet);

    }

    public Bubble(float mDuration = 3)
    {
        durition = mDuration;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;
        //  UILogo = (Sprite)Resources.Load("UI Icons/Spells/FireUI");
    }
}
