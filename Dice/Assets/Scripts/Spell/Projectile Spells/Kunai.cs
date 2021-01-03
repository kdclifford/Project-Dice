﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.Kunai);
    }

    public Kunai()
    {
        duration = 3;
        castingColour = Color.white;
        element = EElementalyType.Physical;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;
        SpellName = "LightNinja";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Kunai", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "PhysicalUI");
        destroyInstantiante = Resources.Load(PathDeath + "SparkDestroy", typeof(GameObject)) as GameObject;
    }
}
