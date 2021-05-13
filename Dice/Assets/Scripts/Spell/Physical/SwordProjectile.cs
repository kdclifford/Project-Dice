﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.SwordProjectile);
    }

    public SwordProjectile()
    {
        range = 20;
        duration = 3;
        damage = 15;
        unlockTier = 2;
        manaCost = 8;
        castingColour = Color.white;
        element = EElementalyType.Physical;
        castingSound = ESoundClipEnum.DrawnSwordSFX;
        spellType = ESpellType.Projectile;
        SpellName = "BigKnifie";
        deathSound = ESoundClipEnum.BluntHitSFX;

        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SwordSpell", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "PhysicalUI");
        destroyInstantiante = Resources.Load(PathDeath + "PhysicalDestroySpark", typeof(GameObject)) as GameObject;
    }
}
