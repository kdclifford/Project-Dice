﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBall : SpellBase
{
    public float projectileSpeed = 500;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.GrassBall);
    }

    public GrassBall()
    {
        damage = 15;
        duration = 3;
        manaCost = 8;
        unlockTier = 1;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.GrassOrbSFX;
        spellType = ESpellType.Projectile;
        SpellName = "GrassBall";
        deathSound = ESoundClipEnum.GrassCrumpleSFX;
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "GrassBall", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "GrassDestroy", typeof(GameObject)) as GameObject;
    }

}
