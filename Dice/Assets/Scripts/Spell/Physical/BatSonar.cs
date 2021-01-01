using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSonar : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.BatSonar);
    }

    public BatSonar()
    {
        duration = 2;
        castingColour = Color.white;
        element = EElementalyType.Physical;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.Bubble;
        SpellName = "EEEEK EEEK";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "BatSonar", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "PhysicalUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
