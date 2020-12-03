using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, Quaternion playerRot, string tag = "Equipped")
    {
        ProjectileFire(posistion, playerRot, tag, projectileSpeed);
        //soundManager.Play(castingSound, bullet);

    }

    public Spark()
    {
        durition = 3;
        castingColour = Color.yellow;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        //  UILogo = (Sprite)Resources.Load("UI Icons/Spells/FireUI");
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/Electric", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/LightningUI");
    }

}
