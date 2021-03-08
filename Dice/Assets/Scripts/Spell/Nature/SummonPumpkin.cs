﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPumpkin : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        //Summon A Pumkin 
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonPumpkin);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "PumpkinSpell", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonPumpkin()
    {
        duration = 5;
        damage = 30;
        unlockTier = 5;
        castingColour = Color.red;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.NoSound;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.Bubble;
        SpellName = "PumpKing";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }
}
