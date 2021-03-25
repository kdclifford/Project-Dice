using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterKnife : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.StarterKnife);
        //soundManager.Play(castingSound, bullet);
    }

    public StarterKnife()
    {
        range = 16;
        unlockTier = 0;
        duration = 3;
        damage = 10;

        castingColour = Color.white;
        element = EElementalyType.Physical;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;
        SpellName = "Shankie";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "KnifeSpell1", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "PhysicalUI");
        destroyInstantiante = Resources.Load(PathDeath + "PhysicalDestroySpark", typeof(GameObject)) as GameObject;
    }
}
