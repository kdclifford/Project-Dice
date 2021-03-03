using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusWater : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.CactusWater);
    }

    public CactusWater()
    {
        damage = 15;
        unlockTier = 1;
        duration = 2;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.CHANGEME;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.CHANGEME;
        SpellName = "Cact Splat";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "CactusWater", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "WaterUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
