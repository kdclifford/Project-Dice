using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, Quaternion playerRot, string tag = "Equipped")
    {
        ProjectileFire(posistion, playerRot, tag, projectileSpeed);
        //soundManager.Play(castingSound, bullet);
    }

    public Bubble(float mDuration = 3)
    {
        durition = mDuration;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;      
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/Bubble", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/WaterUI");
    }

}
