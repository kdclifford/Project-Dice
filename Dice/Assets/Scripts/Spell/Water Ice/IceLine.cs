using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLine : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.IceLine);
    }

    public IceLine()
    {
        duration = 1;
        damage = 12;
        unlockTier = 2;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.SnowConeSFX;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "ChillyBill";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "IceLine", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "IceUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
