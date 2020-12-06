﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireBall : SpellBase
{
    public float projectileSpeed = 700;
    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        ProjectileFire(posistion, rot, tag, projectileSpeed, ESpellEnum.FireBall);

        //soundManager.Play(castingSound, bullet);

    }

    public FireBall()
    {
        duration = 3;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;        
        //  UILogo = (Sprite)Resources.Load("UI Icons/Spells/FireUI");
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/FireBall", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/FireUI");
        destroyInstantiante = Resources.Load("Player/GroundExplosion", typeof(GameObject)) as GameObject;
    }

}
