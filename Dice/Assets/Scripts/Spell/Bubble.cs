﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        ProjectileFire(posistion, rot, tag, projectileSpeed, ESpellEnum.Bubble);
        //soundManager.Play(castingSound, bullet);
    }

    public Bubble()
    {
        duration = 3;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;      
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/Bubble", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/WaterUI");
        destroyInstantiante = Resources.Load("Player/GroundExplosion", typeof(GameObject)) as GameObject;
    }

}
