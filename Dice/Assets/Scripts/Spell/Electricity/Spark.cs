﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.Spark);
        //soundManager.Play(castingSound, bullet);

    }

    public Spark()
    {
        manaCost = 5;
        range = 12;
        unlockTier = 0;
        damage = 10;
        duration = 3;
        castingColour = Color.yellow;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        SpellName = "LekyTingle";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Electric", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "SparkDestroy", typeof(GameObject)) as GameObject;
    }

}
