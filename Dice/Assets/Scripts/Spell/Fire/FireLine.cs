using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.FireCone);
    }

    public FireLine()
    {
        duration = 3;
        damage = 20;
        unlockTier = 2;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.flamestrike;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "PSHHHHFire";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "FireLine", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
