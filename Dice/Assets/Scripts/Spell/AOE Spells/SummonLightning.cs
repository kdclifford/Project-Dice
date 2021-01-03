﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonLightning : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        //Summon A Pumkin 
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonLightning);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SummonLightning", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonLightning()
    {
        duration = 10;
        castingColour = Color.blue;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.Electric;
        SpellName = "ThorMaad";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }
}
