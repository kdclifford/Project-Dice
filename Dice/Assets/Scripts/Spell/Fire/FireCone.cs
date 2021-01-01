using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCone : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.FireCone);
    }

    public FireCone()
    {
        duration = 3;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "PSHHHHFire";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "FireCone", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
