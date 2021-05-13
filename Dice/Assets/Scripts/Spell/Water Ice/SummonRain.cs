using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRain : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        //Summon A Pumkin 
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonRain);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SummonRain", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "WaterUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonRain()
    {
        duration = 10;
        damage = 20;
        unlockTier = 3;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.RainSFX;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "RainyDay";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }
}
