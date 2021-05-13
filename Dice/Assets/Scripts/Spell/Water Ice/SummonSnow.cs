using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSnow : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonSnow);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SummonSnow", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "IceUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonSnow()
    {
        duration = 5;
        damage = 25;
        unlockTier = 3;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.BlizzardSFX;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "SnowwyDay";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }
}
